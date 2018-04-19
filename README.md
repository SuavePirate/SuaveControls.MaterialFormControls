# SuaveControls.MaterialFormControls
Some Xamarin.Forms input fields built to Material Design Standards

This project is still a work in progress and bound to see updates.

## Installation
[![NuGet version](https://badge.fury.io/nu/MaterialFormControls.svg)](https://badge.fury.io/nu/MaterialFormControls)

Use the preview package from NuGet or clone the repository and reference the projects directly:

```
Install-Package MaterialFormControls -Version 2018.2.22-pre1
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
 <suave:MaterialButton x:Name="MyButton" 
      BackgroundColor="#03A9F4" 
      TextColor="White" 
      Text="Click to raise elevation" 
      Elevation="1" 
      VerticalOptions="Center" 
      HorizontalOptions="Center"
      WidthRequest="300"
      BorderRadius="0"
      Clicked="MyButton_Clicked"/>
```

``` csharp
BorderlessEntry.Init();
```

Check out the example projects for more in-depth demos.

## Validation and State

Check out this blog post for more details https://alexdunn.org/2018/02/22/xamarin-controls-material-form-control-updates/

The controls now allow for an `IsValid` state as well as updated color properties: `DefaultColor`, `AccentColor`, `InvalidColor`.

This means you can create custom `Behaviors` that manage the state of the control, or you can manage it through bindings to your ViewModel.

Check out the MaterialFormsBehaviors (https://github.com/SuavePirate/SuaveControls.MaterialEntryBehaviors) repository for examples and helpful Behaviors (work in progress)

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
- ArtjomP (https://github.com/ArtjomP)
