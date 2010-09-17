package org.soitoolkit.commons.log.web;

import javax.servlet.ServletContext;

import org.apache.log4j.Logger;
import org.apache.log4j.PropertyConfigurator;
import org.apache.log4j.helpers.LogLog;
import org.apache.log4j.xml.DOMConfigurator;
import org.soitoolkit.commons.log.Log4jUtils;

/**
 * Utility methods for using log4j in a Java EE Servlet container
 * 
 * @author Tony Dalbrekt
 */
public final class Log4jWebUtils {
    public static Logger logger = Logger.getLogger(Log4jWebUtils.class);

    /**
     * Extension for parameter and property-name for "watch delay"
     */
    public static final String WATCH_DELAY_EXTENSION = ".watch.delay";

    /**
     * Extension for parameter and property-name for "watch disable"
     */
    public static final String WATCH_DISABLE_EXTENSION = ".watch.disable";

    /**
     * Key for {@code context-param} where the value is the name of the JVM-paramter
     * that points out {@code log4j.xml}.
     */
    public static final String CONTEXT_PARAM_LOG_CONFIG_KEY = "log4j.configuration.key";

    /**
     * Key for {@code context-param} used to activate and deactivate automatic relaod of log4j configuration files (default {@code true}). 
     * Valid values are {@code true/false}.
     */
    public static final String CONTEXT_PARAM_LOG_WATCH_DISABLE_KEY = Log4jUtils.DEFUALT_LOG_CONFIG
            + WATCH_DISABLE_EXTENSION;

    /**
     * Key for {@code context-param} to control how often log4j should check the configuration file for changes. (default 60000 ms = 1 minute).
     * Value specified in milliseconds.
     */
    public static final String CONTEXT_PARAM_LOG_WATCH_DELAY_KEY = Log4jUtils.DEFUALT_LOG_CONFIG
            + WATCH_DELAY_EXTENSION;

    /** Protocol prefix */
    public static final String LOG_FILE_PREFIX = "file:/";

    /**
     * Dold konstruktor.
     */
    private Log4jWebUtils() {
        throw new UnsupportedOperationException("Ej till�tet att skap en instans av denna klass");
    }

    /**
     * Kontrollerar om servlet context �r konfigurerad att ladda om
     * konfigurationsfilen till log4j automatiskt vilket �r default-beteendet.
     * <p>
     * F�r att avaktivera <i>auto reload</i> s� konfigurera {@code
     * context-param} {@link #CONTEXT_PARAM_LOG_WATCH_DISABLE_KEY} eller
     * JVM-parameter {@code log4j.configuration.<app_name>.watch.disable}.
     * JVM-parameter �verlagrar ev. konfigurerad {@code context-param}.
     * <p>
     * <i>app_name</i> �r detsamma som konfigurerats f�r att peka ut
     * konfigurationsfilen f�r log4j. Ex. {@code log4j.configuration.volvofkort}
     * d�r volvokort d� �r app_name. Finns inget app_name utg�r den d� s� att
     * parametern bli {@code log4j.configuration.watch.disable}.
     * 
     * <p>
     * Observera att om inget app_name definieras sl�r konfigureringen globalt i
     * JVM:en s� om fler applikationer i samma JVM anv�nder sig av funktionen
     * kan o�nskat beteende uppst�.
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
     * @return {@code false} om {@code context-param}
     *         {@link #CONTEXT_PARAM_LOG_WATCH_DISABLE_KEY} �r satt till {@code
     *         false} annars {@code true}
     */
    public static boolean isWatchDisabled(ServletContext sc) {

        Boolean systemPropertyDisable = getIsWatchDisabledSystemProperty(getLogConfigurationKey(sc)
                + WATCH_DISABLE_EXTENSION);

        // Override web.xml config
        if (systemPropertyDisable != null) {
            return systemPropertyDisable.booleanValue();
        }

        Boolean contextParamDisable = getIsWatchDisabledContextParam(sc);

        if (contextParamDisable != null) {
            return contextParamDisable.booleanValue();
        }

        return false;
    }

    /**
     * Returnerar v�rdet f�r konfigurerad {@code context-param}
     * {@link #CONTEXT_PARAM_LOG_WATCH_DISABLE_KEY}. {@code null} om ej
     * konfigurerad eller om v�rdet inte �r ett giltig boolean annars det
     * booleska v�rdet.
     * 
     * @param sc servlet context
     * @return {@code true} eller {@code false}
     */
    static Boolean getIsWatchDisabledContextParam(ServletContext sc) {
        try {

            String val = sc.getInitParameter(CONTEXT_PARAM_LOG_WATCH_DISABLE_KEY);

            if (val == null || (!val.equalsIgnoreCase("true") && !val.equalsIgnoreCase("false"))) {
                return null;
            }

            return Boolean.valueOf(val);

        } catch (Exception e) {
            return null;
        }
    }

    /**
     * Returnerar flagga f�r given JVM-parameter f�r att <i>enable/disable</i>
     * automatisk laddning av konfigurationen f�r log4j. {@code null} om
     * parameter saknas eller om det inte �r ett giltigt booleskt v�rde
     * (true|false)
     * 
     * @param paramName namn p� jvm-parameter
     * @return flagga f�r att <i>enable/disable</i> auto reload
     */
    static Boolean getIsWatchDisabledSystemProperty(String paramName) {
        try {
            String val = System.getProperty(paramName);

            if (val == null || (!val.equalsIgnoreCase("true") && !val.equalsIgnoreCase("false"))) {
                return null;
            }

            return Boolean.valueOf(val);

        } catch (Exception e) {
            return null;
        }
    }

    /**
     * Returnerar tid i millisekunder i hur ofta kontroll om f�r�ndringar i
     * konfigurationsfilen f�r log4j skall ske. Default sker kontroll var 60:e
     * sekund. Konfigurering av delay sker genom att s�tta delay genom {@code
     * context-param} eller via JVM-parameter. JVM-parameter �verlagrar ev.
     * konfigurerad {@code context-param}.
     * <p>
     * <i>app_name</i> �r detsamma som konfigurerats f�r att peka ut
     * konfigurationsfilen f�r log4j. Ex. {@code log4j.configuration.volvofkort}
     * d�r volvokort d� �r app_name. Finns inget app_name utg�r den d� s� att
     * parametern bli {@code log4j.configuration.watch.delay}.
     * 
     * <p>
     * Observera att om inget app_name definieras sl�r konfigureringen globalt i
     * JVM:en s� om fler applikationer i samma JVM anv�nder sig av funktionen
     * kan o�nskat beteende uppst�.
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
     * @param sc servlet context
     * @return frekvens i millesekunder
     */
    public static long getWatchDelay(ServletContext sc) {

        Long systemPropertyDelay = getWatchDelaySystemProperty(getLogConfigurationKey(sc)
                + WATCH_DELAY_EXTENSION);

        // Override web.xml config
        if (systemPropertyDelay != null) {
            return systemPropertyDelay.longValue();

        }

        Long contextParamDelay = getWatchDelayContextParams(sc);

        if (contextParamDelay != null) {
            return contextParamDelay.longValue();

        }

        return Log4jUtils.DEFAULT_WATCH_DELAY;
    }

    /**
     * Returnerar v�rdet f�r konfigurerad {@code context-param}
     * {@link #CONTEXT_PARAM_LOG_WATCH_DELAY_KEY}. {@code null} om ej
     * konfigurerad, v�rdet inte �r ett giltigt heltal eller om v�rdet �r &lt;
     * 1.
     * 
     * @param sc servlet context
     * @return watch delay
     */
    static Long getWatchDelayContextParams(ServletContext sc) {
        try {
            Long delay = Long.valueOf(sc.getInitParameter(CONTEXT_PARAM_LOG_WATCH_DELAY_KEY));
            if (delay < 1) {
                return null;
            }
            return Long.valueOf(sc.getInitParameter(CONTEXT_PARAM_LOG_WATCH_DELAY_KEY));

        } catch (Exception e) {
            return null;
        }
    }

    /**
     * Returnerar watch delay v�rdet f�r given JVM-parameter. {@code null} om
     * given parameter saknas, inte �r ett giltigt heltal eller �r &lt; 1.
     * 
     * <h4>Exempel</h4>
     * 
     * <pre>
     * -Dlog4j.configuration.watch.delay=30000
     * </pre>
     * 
     * S�tter watch delay till 30 sekunder.
     * 
     * @param paramName namn p� JVM-parameter
     * @return delay i millesekunder
     */
    static Long getWatchDelaySystemProperty(String paramName) {
        try {
            Long delay = Long.valueOf(System.getProperty(paramName));

            if (delay != null && delay < 1) {
                delay = null;
            }
            return delay;
        } catch (Exception e) {
            return null;
        }
    }

    /**
     * Kontrollerar om {@code context-param} {@code log4j.configuration.key} �r
     * definierad i {@code web.xml} och att den skiljer sig ifr�n {@code
     * log4j.configuration}.
     * 
     * @param sc servlet context
     * @return {@code true} om den �r definierad i {@code web.xml}, annars
     *         {@code false}
     */
    public static boolean isLog4jConfigurationRedifined(ServletContext sc) {
        return !Log4jUtils.DEFUALT_LOG_CONFIG.equals(getLogConfigurationKey(sc));
    }

    /**
     * Returnerar nyckeln f�r �tkomst av s�kv�gen till loggkonfigurtionsfilen
     * fr�n JVM:ens parametrar. Default {@code log4j.configuration}. Kan
     * overridas genom att definiera {@code context-param} {@code
     * log4j.configuration.key} i {@code web.xml}.
     * 
     * <h3>Exempel</h3>
     * 
     * <pre>
     *  &lt;context-param&gt;
     *      &lt;param-name&gt;log4j.configuration.key&lt;/param-name&gt;
     *      &lt;param-value&gt;log4j.configuration.batchctrl&lt;/param-value&gt;
     *  &lt;/context-param&gt;
     * </pre>
     * 
     * Obs! Gl�m inte att definiera motsvarande s�kv�g som JVM-parameter.
     * 
     * @param ServletContext
     * @return nyckel till JVM-parameter
     */
    public static String getLogConfigurationKey(ServletContext sc) {
        String val = (String) sc.getInitParameter(CONTEXT_PARAM_LOG_CONFIG_KEY);
        if (val == null) {
            val = Log4jUtils.DEFUALT_LOG_CONFIG;
        }        
        LogLog.debug(CONTEXT_PARAM_LOG_CONFIG_KEY + " = " + val);
        return val;
    }

    /**
     * TODO: ML FIX. REMOVE!!!
     * 
     * Returnerar s�kv�gen till konfigurationsfilen f�r log4j. L�ser default
     * fr�n {@code log4j.configuration}. Se
     * {@link #getLogConfigurationKey(ServletContext)} f�r hur man kan l�sa av
     * en annan nyckel.
     * 
     * @param ServletContext
     * @return s�kv�g till log4j.xml/properties
     */
    public static String getLogConfigurationPath(ServletContext sc) {
    	String configFile = getLogConfigurationKey(sc);

        return configFile;
    }

    /**
     * L�ser in konfigurationsfilen som pekas ut default via {@code
     * log4j.configuration} . �nskas n�gon annan nyckel l�sas av f�r s�kv�g till
     * konfigurationsfilen se {@link #getLogConfigurationKey(ServletContext)}.
     * �r fil�ndelsen {@code .xml} laddas den om med {@link DOMConfigurator}
     * annars {@link PropertyConfigurator}.
     * 
     * @param servlet context
     * @return target page
     */
    public static void reload(ServletContext sc) {
        Log4jUtils.reload(getLogConfigurationPath(sc));
    }

    /**
     * L�ser in konfigurationsfil med given s�kv�g samt startar en lyssnare som
     * laddar om konfigurationen automatiskt om konfigurationsfilen �ndras. �r
     * fil�ndelsen {@code .xml} laddas den om med {@link DOMConfigurator} annars
     * {@link PropertyConfigurator}.
     * 
     * @param servlet servlet context
     * @param delay antal millisekunder mellan kontroll av f�r�ndringar, om 0
     *        anv�nds {@link Log4jWebUtils#DEFAULT_WATCH_DELAY}
     */
    public static void reloadAndWatch(ServletContext sc, long delay) {
        Log4jUtils.reloadAndWatch(getLogConfigurationPath(sc), delay);
    }
}
