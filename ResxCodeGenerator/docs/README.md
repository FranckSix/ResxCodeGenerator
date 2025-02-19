﻿
Snippet

Snippet
 
# ResxCodeGenerator
 
Generates strongly typed wrapper classes. Include contants for each string resource. Avoid the usage of "magic string". Ensure code and resource file coherence.
 
The generated classes are named with the same name as their associated resx. These classes can then be used as follows:
 
file : MyResource.en.resx
```html
    @inject IMyResourceLocalizer Localizer
    <span>@Localizer.Hello</span>
```
As the designated solution for localization is to use [IStringLocalizer](https://docs.microsoft.com/dotnet/api/microsoft.extensions.localization.istringlocalizer-1).
 
# Usage
- Install the package 
```
Install-Package ResxCodeGenerator
```
- Define .resx files
- On the paramters of the resx set \<generation action> to "additionnal c# analyzer file"
 
in csproj this look like this
```
<ItemGroup>
  <AdditionalFiles Include="*.en.resx" /> <!--Where en is your base language two letters culture-->
</ItemGroup>
```
❗ *N.B. - Set this option for only one language per resource. Otherwise, the generated classes will conflict.*
 
## Add Interfaces services in App
The package also include a tools to register all Resources Interfaces.
in **Program.cs** 
 
*I'm a bit allergic to magic string. That's kind of why I reference my namespace from code reflection.*😉

    var assemblyName = typeof(<YourNameSpaceRoot>.Resources.<AnyOfGeneratedClass>).Namespace);

 
But it also possible to reference like this
 

    var assemblyName = "<YourNameSpaceRoot>.Resources";

 
Then use the extension method to add resources's Interfaces
 

    using ResxCodeGeneration.Tools;
    ...
    
    builder.Services
	    .AddLocalizedResources(
		    Assembly.GetExecutingAssembly(),
		    assemblyName);
