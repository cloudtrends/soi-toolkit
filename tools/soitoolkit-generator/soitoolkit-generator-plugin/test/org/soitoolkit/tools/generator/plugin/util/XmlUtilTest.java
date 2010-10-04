package org.soitoolkit.tools.generator.plugin.util;


import static junit.framework.Assert.assertEquals;
import static junit.framework.Assert.assertNull;
import static org.soitoolkit.tools.generator.plugin.util.XmlUtil.createDocument;
import static org.soitoolkit.tools.generator.plugin.util.XmlUtil.getDocumentComment;
import static org.soitoolkit.tools.generator.plugin.util.XmlUtil.getFirstValue;
import static org.soitoolkit.tools.generator.plugin.util.XmlUtil.getXPathResult;
import static org.soitoolkit.tools.generator.plugin.util.XmlUtil.lookupParameterValue;

import java.io.InputStream;
import java.util.Date;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import org.w3c.dom.Document;

public class XmlUtilTest {

	private final String nsPrefix = "ns";
	private final String nsURI = "http://maven.apache.org/POM/4.0.0";
	private final String docComment=" \n"+
"DON'T CHANGE THE CONTENT OF THIS COMMENT UNLESS YOU REALLY KNOW WHAT YOU ARE DOING!\n"+
"[soi-toolkit.gen.version=0.1.7-SNAPSHOT]\n"+
"[soi-toolkit.gen.type=services]\n"+
"[soi-toolkit.gen.createDate=2010-09-28T17:48:20.465+02:00]\n"+
"[soi-toolkit.gen.artifactId=sample1]\n";
	
	Document pomDoc = null;
	
	@BeforeClass
	public static void setUpBeforeClass() throws Exception {
	}

	@AfterClass
	public static void tearDownAfterClass() throws Exception {
	}

	@Before
	public void setUp() throws Exception {
		InputStream content = getClass().getClassLoader().getResourceAsStream("org/soitoolkit/tools/generator/plugin/util/pom.xml");
		pomDoc = createDocument(content);
	}

	@After
	public void tearDown() throws Exception {
	}

	@Test
	public void testXMLDate() {
		System.err.println(XmlUtil.convertDateToXmlDate(new Date()));
	}

	@Test
	public void testGetDocComment() {
		String comment = getDocumentComment(pomDoc);
		assertEquals(docComment, comment);
	}

	@Test
	public void testDocCommentProperties() {
		String comment = getDocumentComment(pomDoc);
		assertEquals("0.1.7-SNAPSHOT", lookupParameterValue("soi-toolkit.gen.version", comment));
	}

	@Test
	public void testRegExp() {
		
		String  s = "dfsdfsd [st1=a] [st2=b] dsfsdf [st3=c]";
		Pattern p = Pattern.compile("\\[st1=[^\\]]*\\]");
		Matcher m = p.matcher(s);
		m.find();
    	String f = m.group();
    	int p1 = f.indexOf('=');
    	int p2 = f.indexOf(']');
    	assertEquals("a", f.substring(p1+1, p2));
	}

	@Test
	public void testExtractGroupIdAndArtifactIdFromPom() {

		InputStream content = getClass().getClassLoader().getResourceAsStream("org/soitoolkit/tools/generator/plugin/util/pom.xml");
		Document doc = createDocument(content);

		// XPath Query for showing all nodes value

		String parentArtifactId = getFirstValue(getXPathResult(doc, nsPrefix, nsURI, "/ns:project/ns:parent/ns:artifactId/text()"));
		String parentGroupId    = getFirstValue(getXPathResult(doc, nsPrefix, nsURI, "/ns:project/ns:parent/ns:groupId/text()"));

		String artifactId = getFirstValue(getXPathResult(doc, nsPrefix, nsURI, "/ns:project/ns:artifactId/text()"));
		String groupId    = getFirstValue(getXPathResult(doc, nsPrefix, nsURI, "/ns:project/ns:groupId/text()"));

		assertEquals("sample1-parent",   parentArtifactId);
		assertEquals("org.sample",       parentGroupId);
		assertEquals("sample1-services", artifactId);
		assertNull(                      groupId);
	}

}
