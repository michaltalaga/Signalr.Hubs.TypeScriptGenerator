[![Build status](https://ci.appveyor.com/api/projects/status/uwhg9nwo62kx2fif?svg=true)](https://ci.appveyor.com/project/cjbhaines/signalr-hubs-typescriptgenerator)

# Signalr.Hubs.TypeScriptGenerator
Utility library for generating typescript definitions for Signalr Hubs. This is a fork of [yetanotherchris/SignalrTypeScriptGenerator](https://github.com/yetanotherchris/SignalrTypeScriptGenerator "SignalrTypeScriptGenerator") by [yetanotherchris](https://github.com/yetanotherchris "yetanotherchris"). I have split the packages up into a referenced library and a console app.

Our usage at Genius Sports is to generate the Hub proxies at build time using our [geniussportsgroup/SignalR.ProxyGenerator](https://github.com/geniussportsgroup/SignalR.ProxyGenerator "Proxy Generator") publishing them to our internal NPM feed. We then use this tool to generate TypeScript definitions our those proxies again publishing them to our internal NPM feed. This allows our UI developers to get strongly typed Hub APIs and allows us to do proper Continous Integrtaion between the back end and front end. Move quickly and break fast.


## Installation - Nuget

- [Signalr.Hubs.TypeScriptGenerator](https://www.nuget.org/packages/Signalr.Hubs.TypeScriptGenerator "Signalr.Hubs.TypeScriptGenerator")
- [Signalr.Hubs.TypeScriptGenerator.Console](https://www.nuget.org/packages/Signalr.Hubs.TypeScriptGenerator.Console "Signalr.Hubs.TypeScriptGenerator.Console")

## Usage

### Signalr.Hubs.TypeScriptGenerator
The utility library is simple to use, load any assemblies required and then create a HubTypeScriptGenerator and call 
the *Generate* method. There  are two overloads of the latter:

- *Generate(TypeScriptGeneratorOptions)* - takes on input generator options (described in details below).
- *Generate(bool)* - this overload is deprecated and retained for backward compatibility only. It is supeseded by 
                   the version below. The single boolean argument is equivalent to using 
                   TypeScriptGeneratorOptions.IncludeReferencePaths option (described later below).
                   The *Generate()* or *Generate(false)* is equivalent to *Generate(TypeScriptGeneratorOptions.Default)*.

The *Generate* method returns the TypeScript as a string. Specifiying a pecific assembly to scan is not suported, since 
the SignalR DefaultHubManager is used which looks up for all loaded assemblies.

    var generator = new HubTypeScriptGenerator();
    var typeScript = generator.Generate(TypeScriptGeneratorOptions.Default);

#### TypeScriptGeneratorOptions

Several options are provided to add to enable more control over generated code. The *TypeScriptGeneratorOptions* class
provides the *Default* proeprty which returns the instance initialized with default values. It also provides set of
customization methods designed for *fluent* coding style. The below example should be quite self-describing: 

    var options = TypeScriptGeneratorOptions.Default
        .WithReferencePaths()
        .WithStrictTypes(NotNullableTypeDiscovery.UseRequiredAttribute)
        .WithOptionalMembers(OptionalMemberGenerationMode.UseDataMemberAttribute);

##### IncludeReferencePaths
If **true** specified, adds the following lines to generated code: 

    /// <reference path="../signalr/index.d.ts" />
    /// <reference path="../ jquery / index.d.ts" />

##### OptionalMemberGenerationMode
Indicates if and when contract interface members shall be generated as optional (having the member name decorated 
with **?** suffix).
- OptionalMemberGenerationMode.None - Instructs not to generate optional members (the default value).
- OptionalMemberGenerationMode.UseDataMemberAttribute - Instructs to generate members attributed by 
     *System.Runtime.Serialization.DataMemberAttribute* with the *IsRequired* proeprty set to **false**, as optional.
     Note, if the *IsRequired* property initializer is omitted, it defaults to **false**. 

Example C# code: 

    [DataContract]
    class SampleDto
    {
        [DataMember]
        public string PhoneNumber1 { get; set; }

        [DataMember(IsRequired=false)]
        public string PhoneNumber2 { get; set; }

        [DataMember (IsRequired=true)]
        public string PhoneNumber3 { get; set; }

        // [DataMember] attribute is omitted
        public string PhoneNumber4 { get; set; }
    }


When generated using *OptionalMemberGenerationMode.None*:

    interface SampleDto
    {
        phoneNumber1 : string;
        phoneNumber2 : string;
        phoneNumber3 : string;
        phoneNumber4 : string;
    }
     
When generated using *OptionalMemberGenerationMode.UseDataMemberAttribute*:

    interface SampleDto
    {
        phoneNumber1? : string;
        phoneNumber2? : string;
        phoneNumber3 : string;
        phoneNumber4 : string;
    }

##### GenerateStrictTypes
If **true** specified, instructs to generate strict type definitions by explicitly adding *null* type to the member 
type declaration for nullable members. The default value is **false**. 

This option shall only be used when generated script is expected to be compiled
with TypeScript 2.0 compiler using the *strictNullChecks* compiler option. In such case types of nullable members are 
generated as *union* types, containing primary type, plus *null* type. All the *Nullable<>* and reference* types are 
generated as nullable (see also the option *NotNullableTypeDiscovery* below).
 
Example C# code: 

    class SampleDto
    {
        public string Address { get; set; }
    }

When generated using *GenerateStrictTypes*=**false**:

    interface SampleDto
    {
        address : string;
    }
     
When generated using *GenerateStrictTypes*=**true**::

    interface SampleDto
    {
        address : string | null;
    }

##### NotNullableTypeDiscovery
Identifies the method used to discover members that should not be declared as nullable, where otherwise they would be.
This option is only applicable if the *GenerateStrictTypes* option is set to **true**. Otherwise, it is ignored.

- *NotNullableTypeDiscovery.None* - use default approach (All *Nullable<>* and reference* types generated as nullable).
- *NotNullableTypeDiscovery.UseRequiredAttribute* - treat members attrobiuted with 
  *System.ComponentModel.DataAnnotations.RequiredAttribute* as not=-nullable.

Example C# code: 

    class SampleDto
    {
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int?   ClientCode   { get; set; }
    }

When generated using *NotNullableTypeDiscovery.None*:

    interface SampleDto
    {
        addressLine1 : string;
        addressLine2 : string;
        clientCode : number;
    }
     
When generated using *NotNullableTypeDiscovery.UseRequiredAttribute*:

    interface SampleDto
    {
        addressLine1 : string;
        addressLine2 : string | null;
        clientCode : number | null;
    }

### Signalr.Hubs.TypeScriptGenerator.Console
Can be used to generate TypeScipt by supplying input from command line.

#### Command line options
Options can be specified using short or long names. The short names are single character prepended with hyphen ('**-**').
The long name must be prepended by double hyphen ('*--*'). Below is the list of supported options.

| Option                       | Description 
| ---------------------------- | ----------- 
| -a, --assembly               | **Required**. The path to the assembly (.dll/.exe)
| -o, --outfile                | The path to the file to generate. If this is empty, the output is written to stdout.
| -i, --includeReferencePaths  | Default: *False*. If true, the jquery and signalr typings reference paths will be included.
| -p, --optionalMembers        | Default: *None*. Specifies method to discover members treated as optional: *None* - don't generate optional members; *DataMemberAttribute* - use [DataMember(IsRequired)] attribute.
| -s, --strictTypes            | Default: *False*. If true, union definitions with *null* are generated for nullable types.
| -n, --notNullableTypes       | Default: *None*. Specifies method to discover members treated as not-nullable: RequiredAttribute: - use [Required] attribute. 
| --help                       | Display help screen.

The following  will output the TypeScript to the specified file path using default TypeScript generation options.

    .\Signalr.Hubs.TypeScriptGenerator.Console.exe -a "c:\etc\path-to-myassembly.dll" -o "C:\temp\.myfile.d.ts"

If the output file is not specified the result is written to standard out.

An optional paramater is -i or -includeReferencePaths, see above for details.

**# ONLY COMBATIBLE WITH SIGNALR VERSIONS AT 2.2.1.0 OR EARLIER #**

We have compiled this at verison 2.2.1.0 so in order for the HubManager to recognise your hubs we are using an assembly redirect. If Microsoft release a new version we will need to update this.
 
    <dependentAssembly>
        <assemblyIdentity name="Microsoft.AspNet.SignalR.Core" publicKeyToken="31bf3856ad364e35" culture="neutral" />
        <bindingRedirect oldVersion="0.0.0.0-2.2.1.0" newVersion="2.2.1.0" />
      </dependentAssembly>

### Data Contract Property Name
Sometimes the serialized name of your data contract properties are changed from the actual C# property name. This is done through the DataMember property:

    [DataContract]
    public class SomethingDto
    {
        [DataMember(Name = "iChangedTheName")]
        public string Property1 { get; set; }

        [DataMember]
        public Guid Property2 { get; set; }
    }

This library will respect the DataMember name and use this as the TypeScript property name:
    
    declare module GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts
    {
        interface SomethingDto
        {
            iChangedTheName : string;
            Property2 : System.Guid;
        }
    }

## Example Output

    /// Autogenerated at 2016-11-14 12:44:07 by https://github.com/geniussportsgroup/Signalr.Hubs.TypeScriptGenerator
    /// <reference path="../signalr/index.d.ts" />
    /// <reference path="../ jquery / index.d.ts" />

    // Hubs

    interface SignalR
    {
        hubA : GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.Hubs.HubA;
        hubC : GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.Hubs.HubC;
        hubB : GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.Hubs.HubB;
    }

    // Service contracts

    declare module GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.Hubs
    {
        interface HubA
        {
            server : GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.Hubs.HubAServer;
            client : GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.Hubs.IHubAClient;
        }

        interface HubAServer
        {
            getSomething() : JQueryPromise<GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts.SomethingDto>;
            getInheritedSomething() : JQueryPromise<GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts.InheritedSomethingDto>;
            ping() : JQueryPromise<void>;
        }
        interface HubC
        {
            server : GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.Hubs.HubCServer;
            // TODO: Hub does not have a Client Interface as a generic argument - it is recommended to add one.
            client : any;
        }

        interface HubCServer
        {
            aServerSideMethod() : JQueryPromise<void>;
        }
        interface HubB
        {
            server : GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.Hubs.HubBServer;
            client : GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.Hubs.IHubBClient;
        }

        interface HubBServer
        {
            getOtherSomething() : JQueryPromise<GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts.OtherSomethingDto>;
            doOtherSomethingElse() : JQueryPromise<void>;
        }
    }

    // Clients

    declare module GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.Hubs
    {
        interface IHubAClient
        {
            pong : () => void;
            takeThis : (somethingDto : GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts.SomethingDto) => void;
        }
        interface IHubBClient
        {
            takeOtherThis : (otherSomethingDto : GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts.OtherSomethingDto) => void;
        }
    }

    // Data contracts

    declare module GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts
    {
        interface OtherSomethingDto 
        {
            RequiredString : string | null;
            OptionalDateTime? : Date;
            OptionalInnerSomething? : GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts.InnerSomethingDto;
            RequiredInnerSomething : GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts.InnerSomethingDto;
        }
        interface InnerSomethingDto 
        {
            InnerProperty1? : number;
            innerProperty2? : Date;
            innerProperty3WithCrazyCustomName? : GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts.SomethingEnum;
        }
        interface InheritedSomethingDto extends GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts.SomethingDto
        {
            OptionalInteger? : number;
            NullableInteger : number | null;
            OptionalNullableInteger? : number | null;
        }
        interface SomethingDto 
        {
            iChangedTheName? : string | null;
            requiredString : string | null;
            OptionalGuid? : string | null;
            RequiredGuid : string;
            NullableRequiredGuid : string;
            OptionalInnerSomething? : GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts.InnerSomethingDto;
        }
    }


    // Enums

    declare module GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts
    {
        enum SomethingEnum
        {
            One = 0,
            Two = 1,
            Three = 2,
        }
    }
