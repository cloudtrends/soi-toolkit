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
package org.soitoolkit.tools.generator.model;

import org.soitoolkit.tools.generator.model.enums.MuleVersionEnum;

public class XmlNamespaceModel {

	private IModel model;
	String xsdNsMuleVersion;
	
	public XmlNamespaceModel(IModel model) {
		this.model                  = model;
		MuleVersionEnum muleVersion = model.getMuleVersion();
		if (muleVersion == null) throw new RuntimeException("null mule version!!!");
		this.xsdNsMuleVersion = (muleVersion == null) ? "unknown" : muleVersion.getXsdNsVersion();
	}
	
	/**
	 * Generates xsd namespaces for files of type: ${artifactId}-common.xml
	 * @return
	 */
	public String getCommon() {
		StringBuffer sb = new StringBuffer();
		sb.append("\txmlns=\"http://www.mulesoft.org/schema/mule/core\"\n");
		sb.append("\txmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
		sb.append("\txmlns:spring=\"http://www.springframework.org/schema/beans\"\n");
		sb.append("\txmlns:jms=\"http://www.mulesoft.org/schema/mule/jms\"\n");
		if (model.isJdbc()) {
			sb.append("\txmlns:jdbc=\"http://www.mulesoft.org/schema/mule/jdbc\"\n");
		}
		if (model.isServlet()) {
			sb.append("\txmlns:servlet=\"http://www.mulesoft.org/schema/mule/servlet\"\n");
		}
		if (model.isImap()) {
			sb.append("\txmlns:imap=\"http://www.mulesoft.org/schema/mule/imap\"\n");
		}
		if (model.isPop3()) {
			sb.append("\txmlns:pop3=\"http://www.mulesoft.org/schema/mule/pop3\"\n");
		}
		sb.append("\txmlns:management=\"http://www.mulesoft.org/schema/mule/management\"\n");
		sb.append("\txmlns:json=\"http://www.mulesoft.org/schema/mule/json\"\n");
		sb.append("\txmlns:scripting=\"http://www.mulesoft.org/schema/mule/scripting\"\n");
		sb.append("\txmlns:rest=\"http://www.mulesoft.org/schema/mule/rest\"\n");
		sb.append("\txsi:schemaLocation=\"\n");
		sb.append("\t\thttp://www.springframework.org/schema/beans    http://www.springframework.org/schema/beans/spring-beans-3.1.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/core       http://www.mulesoft.org/schema/mule/core/" + xsdNsMuleVersion + "/mule.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/jms        http://www.mulesoft.org/schema/mule/jms/" + xsdNsMuleVersion + "/mule-jms.xsd\n");
		if (model.isJdbc()) {
			sb.append("\t\thttp://www.mulesoft.org/schema/mule/jdbc       http://www.mulesoft.org/schema/mule/jdbc/" + xsdNsMuleVersion + "/mule-jdbc.xsd\n");
		}
		if (model.isServlet()) {
			sb.append("\t\thttp://www.mulesoft.org/schema/mule/servlet    http://www.mulesoft.org/schema/mule/servlet/" + xsdNsMuleVersion + "/mule-servlet.xsd\n");
		}
		if (model.isPop3()) {
			sb.append("\t\thttp://www.mulesoft.org/schema/mule/pop3       http://www.mulesoft.org/schema/mule/pop3/" + xsdNsMuleVersion + "/mule-pop3.xsd\n");
		}
		if (model.isImap()) {
			sb.append("\t\thttp://www.mulesoft.org/schema/mule/imap       http://www.mulesoft.org/schema/mule/imap/" + xsdNsMuleVersion + "/mule-imap.xsd\n");
		}
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/management http://www.mulesoft.org/schema/mule/management/" + xsdNsMuleVersion + "/mule-management.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/json       http://www.mulesoft.org/schema/mule/json/current/mule-json.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/scripting  http://www.mulesoft.org/schema/mule/scripting/current/mule-scripting.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/rest       http://www.mulesoft.org/schema/mule/rest/current/mule-rest.xsd\n");
		sb.append("\t\">");
		
		return sb.toString();
	}

	/**
	 * Generates xsd namespaces for files of type: ${artifactId}-config.xml
	 * @return
	 */
	public String getServicesOnlyConfig() {
		StringBuffer sb = new StringBuffer();
		sb.append("\txmlns=\"http://www.mulesoft.org/schema/mule/core\"\n");
		sb.append("\txmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
		sb.append("\txmlns:spring=\"http://www.springframework.org/schema/beans\"\n");
		sb.append("\txsi:schemaLocation=\"\n");
		sb.append("\t\thttp://www.springframework.org/schema/beans http://www.springframework.org/schema/beans/spring-beans-3.1.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/core    http://www.mulesoft.org/schema/mule/core/" + xsdNsMuleVersion + "/mule.xsd\n");
		sb.append("\t\">");
		
		return sb.toString();
	}

	/**
	 * Generates xsd namespaces for files of type: ${artifactId}-jdbc-connector.xml
	 * @return
	 */
	public String getJdbcConnector() {
		StringBuffer sb = new StringBuffer();
		sb.append("\txmlns=\"http://www.mulesoft.org/schema/mule/core\"\n");
		sb.append("\txmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
		sb.append("\txmlns:spring=\"http://www.springframework.org/schema/beans\"\n");
		sb.append("\txmlns:jdbc=\"http://www.mulesoft.org/schema/mule/jdbc\"\n");
		sb.append("\txsi:schemaLocation=\"\n");
		sb.append("\t\thttp://www.springframework.org/schema/beans http://www.springframework.org/schema/beans/spring-beans-3.1.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/core    http://www.mulesoft.org/schema/mule/core/" + xsdNsMuleVersion + "/mule.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/jdbc    http://www.mulesoft.org/schema/mule/jdbc/" + xsdNsMuleVersion + "/mule-jdbc.xsd\n");
		sb.append("\t\">");
		
		return sb.toString();
	}

	/**
	 * Generates xsd namespaces for files of type: ${artifactId}-teststubs-and-services-config.xml
	 * @return
	 */
	public String getTeststubsAndServicesConfig() {
		StringBuffer sb = new StringBuffer();
		sb.append("\txmlns=\"http://www.mulesoft.org/schema/mule/core\"\n");
		sb.append("\txmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
		sb.append("\txmlns:spring=\"http://www.springframework.org/schema/beans\"\n");
		sb.append("\txsi:schemaLocation=\"\n");
		sb.append("\t\thttp://www.springframework.org/schema/beans http://www.springframework.org/schema/beans/spring-beans-3.1.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/core    http://www.mulesoft.org/schema/mule/core/" + xsdNsMuleVersion + "/mule.xsd\n");
		sb.append("\t\">");
		
		return sb.toString();
	}

	/**
	 * Generates xsd namespaces for files of type: ${artifactId}-teststubs-only-config.xml
	 * @return
	 */
	public String getTeststubsOnlyConfig() {
		StringBuffer sb = new StringBuffer();
		sb.append("\txmlns=\"http://www.mulesoft.org/schema/mule/core\"\n");
		sb.append("\txmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
		sb.append("\txmlns:spring=\"http://www.springframework.org/schema/beans\"\n");
		sb.append("\txsi:schemaLocation=\"\n");
		sb.append("\t\thttp://www.springframework.org/schema/beans http://www.springframework.org/schema/beans/spring-beans-3.1.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/core    http://www.mulesoft.org/schema/mule/core/" + xsdNsMuleVersion + "/mule.xsd\n");
		sb.append("\t\">");
		
		return sb.toString();
	}

	/**
	 * Generates xsd namespaces for files of type: ${service}-service.xml
	 * @return
	 */
	public String getOnewayService() {
		StringBuffer sb = new StringBuffer();
		sb.append("\txmlns=\"http://www.mulesoft.org/schema/mule/core\"\n");
		sb.append("\txmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
		sb.append("\txmlns:spring=\"http://www.springframework.org/schema/beans\"\n");
 		sb.append("\txmlns:doc=\"http://www.mulesoft.org/schema/mule/documentation\"\n");
		sb.append("\txmlns:vm=\"http://www.mulesoft.org/schema/mule/vm\"\n");
		sb.append("\txmlns:file=\"http://www.mulesoft.org/schema/mule/file\"\n");
		sb.append("\txmlns:ftp=\"http://www.mulesoft.org/schema/mule/ftp\"\n");
		sb.append("\txmlns:sftp=\"http://www.mulesoft.org/schema/mule/sftp\"\n");
		sb.append("\txmlns:jdbc=\"http://www.mulesoft.org/schema/mule/jdbc\"\n");
		sb.append("\txmlns:email=\"http://www.mulesoft.org/schema/mule/email\"\n");
		sb.append("\txmlns:imap=\"http://www.mulesoft.org/schema/mule/imap\"\n");
		sb.append("\txmlns:pop3=\"http://www.mulesoft.org/schema/mule/pop3\"\n");
		sb.append("\txmlns:smtp=\"http://www.mulesoft.org/schema/mule/smtp\"\n");
		sb.append("\txmlns:jms=\"http://www.mulesoft.org/schema/mule/jms\"\n");
		sb.append("\txmlns:scripting=\"http://www.mulesoft.org/schema/mule/scripting\"\n");
		sb.append("\txmlns:jbossts=\"http://www.mulesoft.org/schema/mule/jbossts\"\n");
		sb.append("\txsi:schemaLocation=\"\n");
		sb.append("\t\thttp://www.springframework.org/schema/beans   http://www.springframework.org/schema/beans/spring-beans-3.1.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/core      http://www.mulesoft.org/schema/mule/core/" + xsdNsMuleVersion + "/mule.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/vm        http://www.mulesoft.org/schema/mule/vm/" + xsdNsMuleVersion + "/mule-vm.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/file      http://www.mulesoft.org/schema/mule/file/" + xsdNsMuleVersion + "/mule-file.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/ftp       http://www.mulesoft.org/schema/mule/ftp/" + xsdNsMuleVersion + "/mule-ftp.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/sftp      http://www.mulesoft.org/schema/mule/sftp/" + xsdNsMuleVersion + "/mule-sftp.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/jdbc      http://www.mulesoft.org/schema/mule/jdbc/" + xsdNsMuleVersion + "/mule-jdbc.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/email     http://www.mulesoft.org/schema/mule/email/" + xsdNsMuleVersion + "/mule-email.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/imap      http://www.mulesoft.org/schema/mule/imap/" + xsdNsMuleVersion + "/mule-imap.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/pop3      http://www.mulesoft.org/schema/mule/pop3/" + xsdNsMuleVersion + "/mule-pop3.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/smtp      http://www.mulesoft.org/schema/mule/smtp/" + xsdNsMuleVersion + "/mule-smtp.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/jms       http://www.mulesoft.org/schema/mule/jms/" + xsdNsMuleVersion + "/mule-jms.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/scripting http://www.mulesoft.org/schema/mule/scripting/" + xsdNsMuleVersion + "/mule-scripting.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/jbossts   http://www.mulesoft.org/schema/mule/jbossts/" + xsdNsMuleVersion + "/mule-jbossts.xsd\n");
		sb.append("\t\">");

		return sb.toString();
	}

	/**
	 * Generates xsd namespaces for files of type: ${service}-teststub-service.xml
	 * @return
	 */
	public String getOnewayTeststubService() {
		StringBuffer sb = new StringBuffer();
		sb.append("\txmlns=\"http://www.mulesoft.org/schema/mule/core\"\n");
		sb.append("\txmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
		sb.append("\txmlns:spring=\"http://www.springframework.org/schema/beans\"\n");
		sb.append("\txmlns:file=\"http://www.mulesoft.org/schema/mule/file\"\n");
		sb.append("\txmlns:ftp=\"http://www.mulesoft.org/schema/mule/ftp\"\n");
		sb.append("\txmlns:sftp=\"http://www.mulesoft.org/schema/mule/sftp\"\n");
		sb.append("\txmlns:jdbc=\"http://www.mulesoft.org/schema/mule/jdbc\"\n");
		sb.append("\txmlns:imap=\"http://www.mulesoft.org/schema/mule/imap\"\n");
		sb.append("\txmlns:jms=\"http://www.mulesoft.org/schema/mule/jms\"\n");
		sb.append("\txmlns:vm=\"http://www.mulesoft.org/schema/mule/vm\"\n");
		sb.append("\txsi:schemaLocation=\"\n");
		sb.append("\t\thttp://www.springframework.org/schema/beans http://www.springframework.org/schema/beans/spring-beans-3.1.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/core    http://www.mulesoft.org/schema/mule/core/" + xsdNsMuleVersion + "/mule.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/file    http://www.mulesoft.org/schema/mule/file/" + xsdNsMuleVersion + "/mule-file.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/ftp     http://www.mulesoft.org/schema/mule/ftp/" + xsdNsMuleVersion + "/mule-ftp.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/sftp    http://www.mulesoft.org/schema/mule/sftp/" + xsdNsMuleVersion + "/mule-sftp.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/jdbc    http://www.mulesoft.org/schema/mule/jdbc/" + xsdNsMuleVersion + "/mule-jdbc.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/imap    http://www.mulesoft.org/schema/mule/imap/" + xsdNsMuleVersion + "/mule-imap.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/jms     http://www.mulesoft.org/schema/mule/jms/" + xsdNsMuleVersion + "/mule-jms.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/vm      http://www.mulesoft.org/schema/mule/vm/" + xsdNsMuleVersion + "/mule-vm.xsd\n");
		sb.append("\t\">");

		return sb.toString();
	}

	/**
	 * Generates xsd namespaces for files of type: ${service}--service.xml
	 * @return
	 */
	public String getReqRespService() {
		StringBuffer sb = new StringBuffer();
		sb.append("\txmlns=\"http://www.mulesoft.org/schema/mule/core\"\n");
		sb.append("\txmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
		sb.append("\txmlns:spring=\"http://www.springframework.org/schema/beans\"\n");
 		sb.append("\txmlns:doc=\"http://www.mulesoft.org/schema/mule/documentation\"\n");
		sb.append("\txmlns:vm=\"http://www.mulesoft.org/schema/mule/vm\"\n");
		sb.append("\txmlns:jdbc=\"http://www.mulesoft.org/schema/mule/jdbc\"\n");
		sb.append("\txmlns:cxf=\"http://www.mulesoft.org/schema/mule/cxf\"\n");
		sb.append("\txmlns:jms=\"http://www.mulesoft.org/schema/mule/jms\"\n");
		sb.append("\txmlns:http=\"http://www.mulesoft.org/schema/mule/http\"\n");
		sb.append("\txmlns:mule-xml=\"http://www.mulesoft.org/schema/mule/xml\"\n");
		sb.append("\txmlns:smooks=\"http://www.muleforge.org/smooks/schema/mule-module-smooks\"\n");
		sb.append("\txmlns:json=\"http://www.mulesoft.org/schema/mule/json\"\n");
		sb.append("\txmlns:scripting=\"http://www.mulesoft.org/schema/mule/scripting\"\n");
		sb.append("\txmlns:rest=\"http://www.mulesoft.org/schema/mule/rest\"\n");
		sb.append("\txsi:schemaLocation=\"\n");
		sb.append("\t\thttp://www.springframework.org/schema/beans   http://www.springframework.org/schema/beans/spring-beans-3.1.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/core      http://www.mulesoft.org/schema/mule/core/" + xsdNsMuleVersion + "/mule.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/vm        http://www.mulesoft.org/schema/mule/vm/" + xsdNsMuleVersion + "/mule-vm.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/jdbc      http://www.mulesoft.org/schema/mule/jdbc/" + xsdNsMuleVersion + "/mule-jdbc.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/cxf       http://www.mulesoft.org/schema/mule/cxf/" + xsdNsMuleVersion + "/mule-cxf.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/jms       http://www.mulesoft.org/schema/mule/jms/" + xsdNsMuleVersion + "/mule-jms.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/http      http://www.mulesoft.org/schema/mule/http/" + xsdNsMuleVersion + "/mule-http.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/xml       http://www.mulesoft.org/schema/mule/xml/" + xsdNsMuleVersion + "/mule-xml.xsd\n");
		sb.append("\t\thttp://www.muleforge.org/smooks/schema/mule-module-smooks http://dist.muleforge.org/smooks/schema/mule-module-smooks/1.3/mule-module-smooks.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/json      http://www.mulesoft.org/schema/mule/json/current/mule-json.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/scripting http://www.mulesoft.org/schema/mule/scripting/current/mule-scripting.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/rest      http://www.mulesoft.org/schema/mule/rest/current/mule-rest.xsd\n");
		sb.append("\t\">");

		return sb.toString();
	}

	/**
	 * Generates xsd namespaces for files of type: ${service}--service.xml
	 * @return
	 */
	public String getReqRespTeststubService() {
		StringBuffer sb = new StringBuffer();
		sb.append("\txmlns=\"http://www.mulesoft.org/schema/mule/core\"\n");
		sb.append("\txmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
		sb.append("\txmlns:spring=\"http://www.springframework.org/schema/beans\"\n");
		sb.append("\txmlns:cxf=\"http://www.mulesoft.org/schema/mule/cxf\"\n");
		sb.append("\txmlns:jms=\"http://www.mulesoft.org/schema/mule/jms\"\n");
		sb.append("\txsi:schemaLocation=\"\n");
		sb.append("\t\thttp://www.springframework.org/schema/beans http://www.springframework.org/schema/beans/spring-beans-3.1.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/core    http://www.mulesoft.org/schema/mule/core/" + xsdNsMuleVersion + "/mule.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/cxf     http://www.mulesoft.org/schema/mule/cxf/" + xsdNsMuleVersion + "/mule-cxf.xsd\n");
		sb.append("\t\thttp://www.mulesoft.org/schema/mule/jms     http://www.mulesoft.org/schema/mule/jms/" + xsdNsMuleVersion + "/mule-jms.xsd\n");
		sb.append("\t\">");

		return sb.toString();
	}
}
