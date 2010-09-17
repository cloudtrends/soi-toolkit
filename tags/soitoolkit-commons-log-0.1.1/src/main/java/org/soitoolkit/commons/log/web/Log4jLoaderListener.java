package org.soitoolkit.commons.log.web;

import javax.servlet.ServletContext;
import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;

import org.apache.log4j.LogManager;
import org.apache.log4j.Logger;
import org.apache.log4j.helpers.LogLog;

/**
 * Lyssnare som vid uppstart av context laddar om konfigurationen f�r log4j om
 * {@code context-param} {@code log4j.configuration.key} �r definierad i web.xml
 * samt st�nger ner Log4j {@link LogManager}n n�r servlet context st�ngs ner. Se
 * {@link Log4jWebUtils#getLogConfigurationKey(javax.servlet.ServletContext)}
 * f�r mer information. Om inte annat konfigurerats laddar log4j
 * forts�ttningsvis om sin konfiguration automatiskt om denna f�r�ndrats. F�r
 * att inaktivera detta beteende l�s mer under rubriken <i>Auto reload</i>.
 * <p>
 * Vid uppstart av servlet context sker en kontroll om {@code context-param} med
 * namn {@code log4j.configuration.key} �r definierad. Finns den definierad
 * anv�nds dess v�rde f�r att sl� upp s�kv�gen till konfigurationsfilen f�r
 * log4j genom {@link System#getProperty(key)}. S�kv�gen till konfigurationen
 * f�r log4j kan d�rmed definieras genom JVM:parametrar.
 * </p>
 * <h4>Exempel: Tomcat</h4>
 * 
 * <pre>
 * CATALINA_OPTS = &quot;-Dlog4j.configuration.volvokort=file:/usr/local/volvokort/config/log4j.xml&quot;
 * </pre>
 * 
 * 
 * <h4>Exempel: web.xml</h4>
 * 
 * <pre>
 * &lt;context-param&gt;
 *     &lt;param-name&gt;log4j.configuration.key&lt;/param-name&gt;
 *     &lt;param-value&gt;log4j.configuration.volvokort&lt;/param-value&gt;
 * &lt;/context-param&gt;
 * 
 * &lt;listener&gt;
 *         &lt;listener-class&gt;se.volvofinans.commons.log.web.Log4jLoaderListener&lt;/listener-class&gt;
 * &lt;/listener&gt;
 * </pre>
 * 
 * <h3>Auto reload</h3>
 * 
 * Default laddas konfigurationsfilen f�r log4j om automatiskt om f�r�ndringar
 * skett. F�r att inaktivera detta beteende s� s�tt {@code context-param}
 * {@link Log4jWebUtils#CONTEXT_PARAM_LOG_WATCH_DISABLE_KEY} till {@code true}
 * eller JVM-parameter {@code log4j.configuration.<app_name>.watch.disable} till
 * {@code false}.
 * <p>
 * <b>Obs!</b> JVM-parameter �verlagrar ev. konfigurerad {@code context-param}.
 * <p>
 * <i>app_name</i> �r detsamma som konfigurerats f�r att peka ut
 * konfigurationsfilen f�r log4j. Ex. {@code log4j.configuration.volvofkort} d�r
 * volvokort d� �r app_name. Finns inget app_name utg�r den d� s� att parametern
 * bli {@code log4j.configuration.watch.disable}.
 * 
 * <p>
 * Observera att om inget app_name definieras sl�r konfigureringen globalt i
 * JVM:en s� om fler applikationer i samma JVM anv�nder sig av funktionen kan
 * o�nskat beteende uppst�.
 * 
 * <h4>Exempel web.xml (context-param)</h4>
 * 
 * <pre>
 * &lt;context-param&gt;
 *     &lt;param-name&gt;log4j.configuration.watch.disable&lt;/param-name&gt;
 *     &lt;param-value&gt;true&lt;/param-value&gt;
 * &lt;/context-param&gt;
 * </pre>
 * 
 * <h4>Exempel JVM-parameter</h4>
 * 
 * <pre>
 * -Dlog4j.configuration.watch.disable=true
 * </pre>
 * 
 * <h3>Konfigurera intervall</h3>
 * 
 * Default �r intervallet f�r hur ofta kontroll sker om konfigurationsfilen f�r
 * log4j f�r�ndrats 1 minut. Intervallet kan konfigureras genom {@code
 * context-param} {@link Log4jWebUtils#CONTEXT_PARAM_LOG_WATCH_DELAY_KEY} (anges
 * i millisekunder).
 * <p>
 * <b>Obs!</b> JVM-parameter �verlagrar ev. konfigurerad {@code context-param}.
 * <p>
 * <i>app_name</i> �r detsamma som konfigurerats f�r att peka ut
 * konfigurationsfilen f�r log4j. Ex. {@code log4j.configuration.volvofkort} d�r
 * volvokort d� �r app_name. Finns inget app_name utg�r den d� s� att parametern
 * bli {@code log4j.configuration.watch.delay}.
 * 
 * <p>
 * Observera att om inget app_name definieras sl�r konfigureringen globalt i
 * JVM:en s� om fler applikationer i samma JVM anv�nder sig av funktionen kan
 * o�nskat beteende uppst�.
 * 
 * <h4>Exempel web.xml (contex param)</h4>
 * 
 * <pre>
 * &lt;context-param&gt;
 *     &lt;param-name&gt;log4j.configuration.watch.delay&lt;/param-name&gt;
 *     &lt;param-value&gt;30000&lt;/param-value&gt; &lt;!-- 30 sek. --&gt;
 * &lt;/context-param&gt;
 * </pre>
 * 
 * <h4>Exempel JVM-parameter</h4>
 * 
 * <pre>
 * -Dlog4j.configuration.watch.delay=30000
 * </pre>
 * 
 * @author Tony Dalbrekt
 */
public class Log4jLoaderListener implements ServletContextListener {
    private Logger logger = Logger.getLogger(Log4jLoaderListener.class);

    /**
     * {@inheritdoc}
     * 
     * @see javax.servlet.ServletContextListener#contextDestroyed(javax.servlet.ServletContextEvent)
     */
    public void contextDestroyed(ServletContextEvent sce) {
        logger.info("Shutdown Log4j LogManager...");
        LogManager.shutdown();

    }

    /**
     * {@inheritdoc}
     * 
     * @see javax.servlet.ServletContextListener#contextInitialized(javax.servlet.ServletContextEvent)
     */
    public void contextInitialized(ServletContextEvent sce) {
    	LogLog.debug("Start to initialize log4j");

    	LogLog.debug("Confige CXF to use log4j!");

    	ServletContext sc = sce.getServletContext();
        if (Log4jWebUtils.isWatchDisabled(sc)) {
            Log4jWebUtils.reload(sc);
        } else {
            Log4jWebUtils.reloadAndWatch(sc, Log4jWebUtils.getWatchDelay(sc));
        }
        logger.info("Log4j initialized...");
    }
}
