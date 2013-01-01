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

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertNull;
import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;
import static org.junit.Assert.fail;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

import java.io.File;
import java.security.cert.X509Certificate;
import java.util.Set;

import javax.security.auth.login.LoginException;
import javax.security.auth.x500.X500Principal;

import org.junit.Before;
import org.junit.Test;
import org.soitoolkit.activemq.jaas.SenderIdCertificateLoginModule;

public class SenderIdCertificateLoginModuleTest {
	
	SenderIdCertificateLoginModule module;
	
	@Before
	public void setUp() {
		module = new SenderIdCertificateLoginModule();
		
		module.setPropertyName("2.5.4.5");
		module.setBaseDir(new File("src/test/resources"));
		module.setUsersFilePathname("users.properties");
		module.setGroupsFilePathname("groups.properties");
	}
	
	@Test
	public void getUserNameForCertificates() {
		
		X509Certificate cert = mock(X509Certificate.class);
		X509Certificate[] x509Certificates = new X509Certificate[] {cert};
		
        X500Principal principal = new X500Principal("SERIALNUMBER=123456789-1, CN=test, OU=test, O=test, DC=test, DC=test, C=test");
        when(cert.getSubjectX500Principal()).thenReturn(principal);
		
		try {
			String userName = module.getUserNameForCertificates(x509Certificates);
			assertEquals("USER_A", userName);
		} catch (LoginException e) {
			fail();
		}
	}
	
	@Test
	public void getUserGroups() {
		
		try {
			Set<String> groups = module.getUserGroups("USER_A");

			assertNotNull(groups);
			assertFalse(groups.isEmpty());
			assertEquals(2, groups.size());
			
			assertTrue(groups.contains("GROUP_A1"));
			assertTrue(groups.contains("GROUP_A2"));
		} catch (LoginException e) {
			fail();
		}
	}
	
	@Test
	public void getUserGroupsNotFound() {
		
		try {
			Set<String> groups = module.getUserGroups("USER_Y");
			assertNotNull(groups);
			assertTrue(groups.isEmpty());
		} catch (LoginException e) {
			fail();
		}
	}
	
	@Test
	public void getUserNameForCertificatesUserNotFound() {
		X509Certificate cert = mock(X509Certificate.class);
		X509Certificate[] x509Certificates = new X509Certificate[] {cert};
		
        X500Principal principal = new X500Principal("SERIALNUMBER=123456789-X, CN=test, OU=test, O=test, DC=test, DC=test, C=test");
        when(cert.getSubjectX500Principal()).thenReturn(principal);
		
		try {
			String userName = module.getUserNameForCertificates(x509Certificates);
			assertNull(userName);
		} catch (LoginException e) {
			fail();
		}
	}
	
	
	@Test
	public void getUserNameForCertificatesNoCert() {
		try {
			module.getUserNameForCertificates(null);
			fail();
		} catch (LoginException e) {
			assertEquals("Client certificates not found. Cannot authenticate.", e.getMessage());
		}
	}
	
	@Test
	public void getPropertyFromX500Principal() {
		X509Certificate cert = mock(X509Certificate.class);
		X509Certificate[] x509Certificates = new X509Certificate[] {cert};
		
		X500Principal principal = new X500Principal("SERIALNUMBER=123456789-1, CN=test, OU=test, O=test, DC=test, DC=test, C=test");
		when(cert.getSubjectX500Principal()).thenReturn(principal);
		
		String property = module.getPropertyFromX500Principal(x509Certificates);
		
		assertNotNull(property);
		assertEquals("#130b3132333435363738392d31", property);
	}
	
	@Test
	public void getPropertyFromX500PrincipalNotFound() {
		X509Certificate cert = mock(X509Certificate.class);
		X509Certificate[] x509Certificates = new X509Certificate[] {cert};
		
		X500Principal principal = new X500Principal("SERIALNUMBER=123456789-1, CN=test, OU=test, O=test, DC=test, DC=test, C=test");
		when(cert.getSubjectX500Principal()).thenReturn(principal);
		
		module.setPropertyName("CD");
		
		String property = module.getPropertyFromX500Principal(x509Certificates);
		
		assertNull(property);
	}
}
