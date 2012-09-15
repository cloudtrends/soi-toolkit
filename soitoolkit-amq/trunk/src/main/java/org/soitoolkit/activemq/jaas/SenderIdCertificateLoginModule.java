/* 
 * Licensed to the soi-toolkit project under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The soi-toolkit project licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */	
package org.soitoolkit.activemq.jaas;

import java.io.File;
import java.io.IOException;
import java.security.cert.X509Certificate;
import java.util.Enumeration;
import java.util.HashSet;
import java.util.Map;
import java.util.Properties;
import java.util.Set;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import javax.security.auth.Subject;
import javax.security.auth.callback.CallbackHandler;
import javax.security.auth.login.LoginException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/**
 * A LoginModule allowing for SSL certificate based authentication based on
 * sender-id's stored in text files.
 * 
 * The sender-id's are parsed using a Properties class where each line is
 * <user_name>=<sender-id>. This class also uses a group definition file where
 * each line is <group_name>=<user_name_1>,<user_name_2>,etc.
 * 
 * The user and group files' locations must be specified in the
 * org.soitoolkit.commons.activemq.jaas.users and
 * org.soitoolkit.commons.activemq.jaas.groups respectively. NOTE: This class
 * will re-read user and group files for every authentication (i.e it does live
 * updates of allowed groups and users).
 * 
 */
public class SenderIdCertificateLoginModule extends
		org.apache.activemq.jaas.CertificateLoginModule {

	private static final Logger log = LoggerFactory
			.getLogger(SenderIdCertificateLoginModule.class);

	private static final String USER_FILE = "org.soitoolkit.activemq.jaas.users";
	private static final String GROUP_FILE = "org.soitoolkit.activemq.jaas.groups";
	private static final String PROPERTY_NAME = "org.soitoolkit.activemq.jaas.property";

	private File baseDir;
	private String usersFilePathname;
	private String groupsFilePathname;

	private String propertyName;

	/**
	 * Performs initialization of file paths. A standard JAAS override.
	 */
	@Override
	public void initialize(Subject subject, CallbackHandler callbackHandler,
			Map sharedState, Map options) {
		super.initialize(subject, callbackHandler, sharedState, options);

		log.debug("Initializing...");
		
		if (System.getProperty("java.security.auth.login.config") != null) {
			baseDir = new File(
					System.getProperty("java.security.auth.login.config"))
					.getParentFile();
		} else {
			baseDir = new File(".");
		}

		usersFilePathname = (String) options.get(USER_FILE);
		groupsFilePathname = (String) options.get(GROUP_FILE);
		propertyName = (String) options.get(PROPERTY_NAME);
		
		log.debug("Initialized!");
	}

	/**
	 * Overriding to allow sender-id authorization based on sender-id's specified
	 * in text files. The sender-id property must be specified with
	 * org.soitoolkit.commons.activemq.jaas.property
	 * 
	 * @param certs
	 *            The certificate the incoming connection provided.
	 * @return The user's authenticated name or null if unable to authenticate
	 *         the user.
	 * @throws LoginException
	 *             Thrown if unable to find user file or connection certificate.
	 */
	@Override
	protected String getUserNameForCertificates(X509Certificate[] certs)
			throws LoginException {

		if (certs == null) {
			throw new LoginException(
					"Client certificates not found. Cannot authenticate.");
		}

		String senderId = this.getPropertyFromX500Principal(certs);

		if (senderId == null) {
			return null;
		}
		
		log.debug("Sender ID: " + senderId);
		
		Properties users = this.getUserProperties();
		
		Enumeration<Object> keys = users.keys();
		for (Enumeration<Object> vals = users.elements(); vals
				.hasMoreElements();) {
			if (((String) vals.nextElement()).equals(senderId)) {
				String key = (String) keys.nextElement();
				log.debug("User found! User: " + key + ", Sender ID: " + senderId);
				
				return key;
			} else {
				keys.nextElement();
			}
		}
		return null;
	}
	
	/**
	 * Overriding to allow for group discovery based on text files.
	 * 
	 * @param username
	 *            The name of the user being examined. This is the same name
	 *            returned by getUserNameForCertificates.
	 * @return A Set of name Strings for groups this user belongs to.
	 * @throws LoginException
	 *             Thrown if unable to find group definition file.
	 */
	@Override
	protected Set<String> getUserGroups(String username) throws LoginException {
		
		log.debug("Trying to lookup user group from username, " + username);
		
		Properties groups = this.getGroupProperties();

		Set<String> userGroups = new HashSet<String>();
		for (Enumeration<Object> enumeration = groups.keys(); enumeration
				.hasMoreElements();) {
			String groupName = (String) enumeration.nextElement();
			String[] userList = (groups.getProperty(groupName)).split(",");

			for (int i = 0; i < userList.length; i++) {
				if (username.equals(userList[i])) {
					userGroups.add(groupName);
					
					log.debug("User group found, " + groupName);
					break;
				}
			}
		}
		
		return userGroups;
	}
	
	protected Properties getUserProperties() throws LoginException {
		
		log.debug("Trying to load user properties...");
		log.debug("Directory:) + baseDir");
		log.debug("Filename: " + usersFilePathname);
		
		File usersFile = new File(baseDir, usersFilePathname);

		Properties users = new Properties();
		try {
			java.io.FileInputStream in = new java.io.FileInputStream(usersFile);
			users.load(in);
			in.close();
			
			log.debug("User properties loaded successfully");
			
			return users;
		} catch (IOException ioe) {
			throw new LoginException("Unable to load user properties file "
					+ usersFile);
		}
	}
	
	protected Properties getGroupProperties() throws LoginException {
		
		log.debug("Trying to load group properties...");
		log.debug("Directory:) + baseDir");
		log.debug("Filename: " + groupsFilePathname);
		
		File groupsFile = new File(baseDir, groupsFilePathname);

		Properties groups = new Properties();
		try {
			java.io.FileInputStream in = new java.io.FileInputStream(groupsFile);
			groups.load(in);
			in.close();
			
			log.debug("Group properties loaded successfully");
			
			return groups;
		} catch (IOException ioe) {
			throw new LoginException("Unable to load group properties file "
					+ groupsFile);
		}
	}

	protected String getPropertyFromX500Principal(X509Certificate[] x509Certificates) {

		X509Certificate cert = null;

		if (x509Certificates.length > 0 && x509Certificates[0] != null) {
			cert = (X509Certificate) x509Certificates[0];
			
			log.debug("Certificate found.");

			String principalName = cert.getSubjectX500Principal().getName();
			
			log.debug("Principal Name: " + principalName);

			
			log.debug("Searching for property: " + propertyName);
			
			Pattern pattern = Pattern.compile(propertyName + "=([^,]+)");
			Matcher matcher = pattern.matcher(principalName);
			
			if (matcher.find()) {
				String match = matcher.group(1);
				log.debug("Property found: " + match);
				return match;
			}
		}
		return null;
	}
	
	public void setBaseDir(File baseDir) {
		this.baseDir = baseDir;
	}

	public void setUsersFilePathname(String usersFilePathname) {
		this.usersFilePathname = usersFilePathname;
	}

	public void setGroupsFilePathname(String groupsFilePathname) {
		this.groupsFilePathname = groupsFilePathname;
	}

	public void setPropertyName(String propertyName) {
		this.propertyName = propertyName;
	}
}
