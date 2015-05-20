# Introduction #
TBD

**Since 0.6.0**


# Details #

The declaration should be placed in the common-xml-config-file: `src/main/app/${artifactId}-common.xml`.

```
   <spring:bean id="senderIdEvaluator" class="org.soitoolkit.commons.mule.cert.X509CertificateEvaluator">
	<spring:property name="propertyName" value="${SENDERID_PROPERTY}" />
   </spring:bean>
	
   <custom-transformer name="validateSenderId" class="org.soitoolkit.commons.mule.cert.ValidateSenderIdTransformer">
      <spring:property name="senderIdPropertyName" value="${SENDERID_PROPERTY}"/>
      <spring:property name="validSenderIds"  value="${VALID_SENDERIDS}"/>
   </custom-transformer>
```

The properties above should be placed in the property-file: `src/main/resources/${artifactId}-config.properties`.

```
# Certificate configuration
SENDERID_PROPERTY=<Property>
VALID_SENDERIDS=<Sender A>,<Sender B>
```


The final step is to introduce the `validateSenderId` transformer to the https:inbound-endpoint:

```
  <flow name="https2https-service">

     <https:inbound-endpoint
            connector-ref="soitoolkit-https-connector"
			address="${HTTPS2HTTPS_INBOUND_URL}"
			exchange-pattern="request-response"
			transformer-refs="objToStr logReqIn validateSenderId"
			responseTransformer-refs="createSoapFaultIfException logRespOut">
      </https:inbound-endpoint>

      ...
  </flow>
```

It also possible to use the `senderIdEvaluator` directly, something that can be useful when it comes to logging. Following example is how it can look like when logging the sender id as `extraInfo`:

```
  <custom-transformer name="logReqIn" class="org.soitoolkit.commons.mule.log.LogTransformer">
     <spring:property name="logType"  value="req-in"/>
	<spring:property name="jaxbObjectToXml"  ref="objToXml"/>
	<spring:property name="extraInfo">
           <spring:map>
              <spring:entry key="Sender" value="#[x509cert:sender-id]"/>
           </spring:map>
     </spring:property>
  </custom-transformer>
```