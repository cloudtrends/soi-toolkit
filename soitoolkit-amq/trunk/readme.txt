1. Copy the plugin (jar) file to ${activemq_home}/lib
2. Copy/Create login.config, users.properties, groups.properties (under ${activemq_home}/conf folder)

2.1 login.properties
SenderIdCertificateLoginModule {
        org.soitoolkit.activemq.jaas.SenderIdCertificateLoginModule required
        org.soitoolkit.activemq.jaas.users="users.properties"
        org.soitoolkit.activemq.jaas.groups="groups.properties"
        org.soitoolkit.activemq.jaas.property="OU";
};

2.2 users.properties
<user>=<property_value>

Example:
user_x=SE12334454-12232323

2.3 group.properties 
<group a>=<user a>,<user b>
<group b>=<user x>,<user a>

Example:
group_x=user_x

3. Update activemq.xml

3.1 SSL Context + SSL Connector to enable SSL communication
 
 Example:
 <sslContext>
    <sslContext
        keyStore="file:${activemq.base}/conf/cert/keystore.jks"
        keyStorePassword="password"
        trustStore="file:${activemq.base}/conf/cert/truststore.jks"
        trustStorePassword="password"/>
    </sslContext>
    
    ...
    
    <transportConnector name="ssl" uri="ssl://localhost:61618?needClientAuth=true"/>

3.2 plugins

3.2.1 Custom JAAS plugin + Authorization plugin

   Example:
   
   <plugins>
   		<!-- Custom JAAS plugin -->
        <jaasCertificateAuthenticationPlugin configuration="SenderIdCertificateLoginModule" />
        
        <!-- Authorization Plugin -->
        <authorizationPlugin>
                <map>
                        <authorizationMap>
                                <authorizationEntries>
                                        <authorizationEntry queue=">" read="admins" write="admins" admin="admins" />
                                        <authorizationEntry topic=">" read="admins" write="admins" admin="admins" />

                                        <authorizationEntry queue="MM.>" read="group_x" write="group_x" admin="group_x" />
                                        <authorizationEntry topic="ActiveMQ.Advisory.>" read="group_x" write="group_x" admin="group_x"/>
                                </authorizationEntries>
                        </authorizationMap>
                </map>
        </authorizationPlugin>
	</plugins>
	
	
