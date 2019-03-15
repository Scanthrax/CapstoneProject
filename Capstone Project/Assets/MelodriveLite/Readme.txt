       __  ___    __        __    _
      /  |/  /__ / /__  ___/ /___(_)  _____
     / /|_/ / -_) / _ \/ _  / __/ / |/ / -_)
    /_/  /_/\__/_/\___/\_,_/_/ /_/|___/\__/


# Melodrive Lite Unity SDK v0.4.1

**Please note that this is beta software and so there may be bugs and crashes!**

## Overview

Melodrive is an AI adaptive music generation system for interactive media.
This package contains the Melodrive Lite Audio Plugin for Unity Editor.

Melodrive Lite is a restricted demo version of Melodrive Indie.
Head to melodrive.com to unlock Melodrive Indie Beta and gain access to a whole host of additional features!
http://melodrive.com/melodriveIndie.php

## Quick Start

1) Add the Melodrive Prefab (in `Melodrive/Resources`) to your scene.
2) (optional) Enter style and emotion parameters
3) Click "Play On Start" checkbox on the prefab.
4) Enter play mode!

## Documentation

The C# script reference is found in the Documentation folder.

There is a series of video tutorials on our YouTube channel (https://www.youtube.com/watch?v=fQeoPf9ivL4&list=PLyOZFq1fEfUFfFWn0fSsp5Ev73ahUGJqC).

## Package Contents

The included assets are structured as follows:
- Melodrive
    - Branding - This folder contains images for the Melodrive splash screen
    - Documentation - This folder contains the C# script reference
    - Examples - This folder contains a number of different examples, each showing one Melodrive feature
    - Mixers - This is where the Melodrive Mixer is stored
    - Plugins - This is where the native plugins are stored for the various platforms
    - Resources - This contains the Melodrive Prefab, which needs to be added to every scene using Melodrive
    - Scripts - This folder contains various Scripts Melodrive uses to run
    - Tests - This folder contains Melodrive's unit tests
- StreamingAssets
    - MelodriveInstruments - This is where all the instruments are stored

## Examples

This package includes a set of example scenes to show you how Melodrive works.
It's a good idea to start by looking in here.
    
## Features

### Style
Melodrive generates music in certain `Styles`, these Styles dictate the overall musical feel.
The Styles available in this release are:
- `ambient` - a slow and meditative style
- `house` - an electronic dance music style
- `piano` - a simple solo piano style
- `rock` - a classic rock band style

Use `SetStyle` to change the active style.

### Emotion
Within a Style, the music will adapt to the given emotion.

Use the `SetEmotion` method on the Melodrive Prefab to set emotion to many different states.

### Musical Seed

Musical Seeds are the core compositional elements of the music.
This is the core musical material (melody/chords) that Melodrive adapts infinitely.
They can be shared across Styles, which ensures that the overall musical content is be the same, but certain style-specific aspects will be different e.g. what instruments are being used.
We recommend having a play with the same Musical Seed across different styles to get an idea of how this works in practice.

Use `SetMusicalSeed` to change the active Musical Seed.

### Misc controls

- `Play`, `Pause` and `Stop` - call these to start/pause/stop playback
- `SetTempoScale` - you can speed up or slow down the tempo with this simple tempo scale control.
E.g. If you want the music to be twice as fast, pass in a value of 2.
- `SetMasterGain` - sets the overall gained output of the Music. Useful for when you want to duck the audio to hear other sources such as speech or effects.
- `SetLimiterEnabled` - by default, Melodrive "limits" the audio sent to Unity, so that there's no distortion. You can enable/disable this feature here.

## Software Requirements

OS: Windows 10 64-bit and Mac OS X 10.13 (High Sierra) or greater
Unity: Unity 2018.2+
CPU: > Intel i5 @ 2GHz recommended
RAM: > 4 GB RAM recommended

## License

Melodrive Lite Beta is a Restricted Asset pursuant to the Unity Asset Store EULA.
Your use of this package is governed by the Melodrive Lite SDK Developer and Software License Agreement (http://melodrive.com/LiteDeveloperLicenseAgreement.html).
