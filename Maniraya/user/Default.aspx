<configuration>
  <configSections>
    <section name="validation" type="Microsoft.Practices.EnterpriseLibrary.Validation.Configuration.ValidationSettings, Microsoft.Practices.EnterpriseLibrary.Validation, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
    <section name="securityConfiguration" type="Microsoft.Practices.EnterpriseLibrary.Security.Configuration.SecuritySettings, Microsoft.Practices.EnterpriseLibrary.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
    <section name="policyInjection" type="Microsoft.Practices.EnterpriseLibrary.PolicyInjection.Configuration.PolicyInjectionSettings, Microsoft.Practices.EnterpriseLibrary.PolicyInjection, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
    <section name="loggingConfiguration" type="Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.LoggingSettings, Microsoft.Practices.EnterpriseLibrary.Logging, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
    <section name="exceptionHandling" type="Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration.ExceptionHandlingSettings, Microsoft.Practices.EnterpriseLibrary.ExceptionHandling, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
    <section name="dataConfiguration" type="Microsoft.Practices.EnterpriseLibrary.Data.Configuration.DatabaseSettings, Microsoft.Practices.EnterpriseLibrary.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
    <section name="instrumentationConfiguration" type="Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation.Configuration.InstrumentationConfigurationSection, Microsoft.Practices.EnterpriseLibrary.Common, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
    <section name="securityCryptographyConfiguration" type="Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.Configuration.CryptographySettings, Microsoft.Practices.EnterpriseLibrary.Security.Cryptography, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
    <section name="cachingConfiguration" type="Microsoft.Practices.EnterpriseLibrary.Caching.Configuration.CacheManagerSettings, Microsoft.Practices.EnterpriseLibrary.Caching, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
    <section name="enterpriseLibrary.ConfigurationSource" type="Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ConfigurationSourceSection, Microsoft.Practices.EnterpriseLibrary.Common, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
  </configSections>
  <dataConfiguration defaultDatabase="Connection String" />
  <connectionStrings>
	  <add name="Connection String" connectionString="Server=184.95.53.181;Initial Catalog=www_indigrowmpl_Demo;  USER ID=www_indigrowmpl_Demo1;PASSWORD=nr#sqRoE06lod0Jq; trusted_connection=false;" providerName="System.Data.SqlClient" />
    <!--<add name="Connection String" connectionString="Server=.\sqlexpress;Initial Catalog=raxtonDb; trusted_connection=true;" providerName="System.Data.SqlClient"/>-->
  </connectionStrings>
  <appSettings>
    <add key="apiBaseUrl" value="http://stx/"/>
    <add key="UMobileNo" value="915863"/>
    <add key="Password" value="gdNmBFlQ=="/>
  </appSettings>
  <enterpriseLibrary.ConfigurationSource selectedSource="System Configuration Source">
    <sources>
      <add name="System Configuration Source" type="Microsoft.Practices.EnterpriseLibrary.Common.Configuration.SystemConfigurationSource, Microsoft.Practices.EnterpriseLibrary.Common, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
    </sources>
  </enterpriseLibrary.ConfigurationSource>
  <system.web>
    <webServices>
      <protocols>
        <add name="HttpGet"/>
        <add name="HttpPost"/>
      </protocols>
    </webServices>
    <httpHandlers>
      <add path="Reserved.ReportViewerWebControl.axd" verb="*" type="Microsoft.Reporting.WebForms.HttpHandler, Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
        validate="false" />
    </httpHandlers>
    <globalization culture="en-US" uiCulture="en-US" />
    <sessionState mode="InProc" timeout="5550" />
    <httpRuntime requestValidationMode="2.0" />
    <compilation debug="true" targetFramework="4.5">
      <assemblies>
        <add assembly="System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" />
        <add assembly="System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A" />
        <add assembly="System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089" />
        <add assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" />
        <add assembly="Microsoft.ReportViewer.Common, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" />
        <add assembly="Microsoft.Build.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A" />
      </assemblies>
      <buildProviders>
        <add extension=".rdlc" type="Microsoft.Reporting.RdlBuildProvider, Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" />
      </buildProviders>
    </compilation>
    <!--<sessionState mode="InProc" cookieless="false" timeout="3600"></sessionState>-->
    <pages controlRenderingCompatibilityVersion="3.5" clientIDMode="AutoID" />
	  <customErrors mode="Off" />
  </system.web>
  <system.webServer>
    <validation validateIntegratedModeConfiguration="false" />
    <handlers>
      <add name="ReportViewerWebControlHandler" preCondition="integratedMode" verb="*" path="Reserved.ReportViewerWebControl.axd" type="Microsoft.Reporting.WebForms.HttpHandler, Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" />
    </handlers>
  </system.webServer>
</configuration>