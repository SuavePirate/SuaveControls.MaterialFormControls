# SuaveControls.MaterialFormControls
Some Xamarin.Forms input fields built to Material Design Standards

This project is still a work in progress and bound to see updates.

## Installation

Use the preview package from NuGet or clone the repository and reference the projects directly:

```
Install-Package MaterialFormControls -Version 2018.1.18-pre1
```

## Usage

Use each of the form controls in your XAML or C# code, and be sure to initialize the underlying `Borderless-Renderer` if you have issues with them being linked out:


``` xml
<suave:MaterialEntry Placeholder="Email" Keyboard="Email" AccentColor="Green"/>
<suave:MaterialEntry Placeholder="Regular Text" AccentColor="Orange"/>
<suave:MaterialEntry Placeholder="Number" Keyboard="Numeric" AccentColor="Red"/>
<suave:MaterialEntry IsPassword="True" Placeholder="Password" AccentColor="Blue"/>
<suave:MaterialDatePicker Placeholder="Date Picker" AccentColor="BlueViolet"/>
<suave:MaterialTimePicker Placeholder="Time Picker" AccentColor="HotPink" />
<suave:MaterialPicker 
    Placeholder="Picker"
    AccentColor="Maroon"
    Items="{Binding PickerData}"
    SelectedItem="{Binding PickerSelectedItem}"
    SelectedIndexChangedCommand="{Binding PickerSelectedIndexChangedCmd}" />
```

``` csharp
BorderlessEntry.Init();
```

Check out the example projects for more in-depth demos.

## Contributing

We are always looking for help to make this project even better!
- Add new controls
- Expand on existing controls
- Fix bugs!

Awesome contributors:
- Me! - Alex Dunn (https://github.com/SuavePirate)
- Greg Gacura (https://github.com/cosmo777)
- welcometoall (https://github.com/welcometoall)
- Evgeny Zborovsky (https://github.com/yuv4ik)

