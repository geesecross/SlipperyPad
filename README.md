# SlipperyPad
A simple tool for Windows that enables the dedicated mouse sensitivity for touchpads.

Whenever you move the mouse cursor via touchpads, SlipperyPad automatically changes the mouse sensitivity as you set. The sensitivity will go back when you stop using touchpads.

SlipperyPad follows MIT license.

## Usage
```
SlipperyPad.exe {normal_speed} {touchpad_speed}
```

 * `normal_speed`: (Integer, `1`-`20`) The sensitivity level of any pointing devices except touchpads.
 * `touchpad_speed`: (Integer, `1`-`20`) The sensitivity level of touchpads.

Note that the default sensitivity of Windows is `10`.

Once you run SlipperyPad, It runs forever in the background. If you want to change the sensitivity setting, you must stop `SlipperyPad.exe` in Task Manager. No other way to stop this app is provided.

## Tested Environment
 * Windows 10 1903H
 * .NET Framework 4.7.2
 * Synaptics SMBus ClickPad in Sony Vaio Pro 13

## Supported Touchpad Vendors
 * Synaptics

Currently, SlipperyPad does not support touchpads of another vendors such as ELAN because I don't have any of them. If you have one of them and you know how to deal with the vendor's SDK, just feel free to give pull request.