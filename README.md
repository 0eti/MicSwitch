> [!IMPORTANT]
> **비공식 포크입니다.** 이 저장소는 [iXab3r/MicSwitch](https://github.com/iXab3r/MicSwitch)의 비공식 포크이며, 원작자(iXab3r)와 관계가 없습니다.
> 원본 프로그램의 저작권은 원작자에게 있습니다. 원작자가 요청하면 이 포크는 내려갑니다.

## 수정사항 (1.0.254, 이 포크에서 바뀐 점)

원본 1.0.253을 기준으로 아래 두 가지만 바꿨습니다.

### 1. 시작할 때 "액세스가 거부되었습니다" 오류로 꺼지는 버그 수정
- **증상:** 컴퓨터를 켤 때 자동 실행되면 `PInvoke.Win32Exception (5): 액세스가 거부되었습니다` 오류 창이 뜨고 프로그램이 꺼짐. 다시 켜면 정상 동작함.
- **원인:** 시작할 때 메인 창을 맨 앞으로 가져오려고(`SetForegroundWindow` → `AttachThreadInput`) 하는데, 로그인 직후에는 Windows 알림 창 같은 시스템 창이 맨 앞에 있어서 Windows가 이를 거부함. 이 예외가 치명적 오류로 처리되어 앱이 종료됨.
- **수정:** 창을 맨 앞으로 가져오는 데 실패하면(`Win32Exception`) 경고 로그만 남기고 계속 실행함. 창은 이미 화면에 표시된 상태라 기능에는 영향이 없음. 시작할 때와 트레이 아이콘으로 창을 열 때 모두 적용. (`MainWindowViewModel.ShowMainWindow`)

### 2. 한글 패치
- MicSwitch 메인 창, 트레이 메뉴, 단축키 편집, 오버레이, 파일 선택 창을 한국어로 번역.
- 공용 라이브러리 PoeShared의 오류 보고 창, 업데이트 화면, 알림음 선택 화면과 상태 메시지도 한국어로 번역. PoeShared 수정분은 [0eti/PoeEye `micswitch-ko` 브랜치](https://github.com/0eti/PoeEye/tree/micswitch-ko)에 있습니다.

### 설치
[Releases](https://github.com/0eti/MicSwitch/releases)에서 받을 수 있습니다. 기존 MicSwitch 설정(`%AppData%\MicSwitch`)은 그대로 사용됩니다.

### 직접 빌드할 때
```
git clone https://github.com/0eti/MicSwitch.git
cd MicSwitch
git submodule update --init Submodules/PoeEye Submodules/PropertyBinder Submodules/gong-wpf-dragdrop
InitSymlinks.cmd
dotnet build Sources\MicSwitch\MicSwitch.csproj -c Release -p:WindowsSdkPackageVersion=10.0.20348.25
```
.NET 6 SDK가 필요합니다. `WindowsSdkPackageVersion`을 지정하지 않으면 최신 WinRT 라이브러리가 섞여 실행 시 오류가 날 수 있습니다. `Submodules/DeploymentTools`는 원본 저장소가 비공개라 받을 수 없지만, 빌드에는 필요 없습니다.

---
![](https://img.shields.io/github/release-date/iXab3r/MicSwitch.svg) ![](https://img.shields.io/github/downloads/iXab3r/MicSwitch/total.svg) ![](https://img.shields.io/github/last-commit/iXab3r/MicSwitch.svg)
[![Discord Chat](https://img.shields.io/discord/513749321162686471.svg)](https://discord.gg/BExRm22)  

# Intro
There are dozens of different audio chat apps like Discord, TeamSpeak, Ventrilo, Skype, in-game audio chats, etc. And all of them have DIFFERENT ways of handling push-to-talk and always-on microphone functionality. I bet many of you know how distracting it could be when someone forgets to turn off a microphone. I will try to explain what I mean using a feature matrix.

| App  | Microphone status overlay | Keyboard support | Mouse buttons support | Audio notification |
| -------------: | :-------------: | :-------------: | :-------------: | :-------------: |
| MicSwitch |  ![Supported](https://i.imgur.com/GOuQvrh.png "Supported") |  ![Supported](https://i.imgur.com/GOuQvrh.png "Supported") |  ![Supported](https://i.imgur.com/GOuQvrh.png "Supported") |  ![Supported](https://i.imgur.com/GOuQvrh.png "Supported")
| Discord  |  In-game only  |   ![Supported](https://i.imgur.com/GOuQvrh.png "Supported")  |  ![Supported](https://i.imgur.com/GOuQvrh.png "Supported")   |  ![Not supported](https://i.imgur.com/AxsV1yJ.png "Not supported") |
| TeamSpeak  |  ![Supported](https://i.imgur.com/GOuQvrh.png "Supported")  |  ![Supported](https://i.imgur.com/GOuQvrh.png "Supported")   |  ![Supported](https://i.imgur.com/GOuQvrh.png "Supported")  |   ![Supported](https://i.imgur.com/GOuQvrh.png "Supported")  |
| Ventrilo  | ![Not supported](https://i.imgur.com/AxsV1yJ.png "Not supported")  |   ![Supported](https://i.imgur.com/GOuQvrh.png "Supported")  |  [Has a bug dating 2012](http://forum.ventrilo.com/showthread.php?t=61203 "Has a bug dating 2012")  |   ![Supported](https://i.imgur.com/GOuQvrh.png "Supported")  |
| Skype  | ![Not supported](https://i.imgur.com/AxsV1yJ.png "Not supported")  |  Hard-coded Ctrl+M  |  ![Not supported](https://i.imgur.com/AxsV1yJ.png "Not supported")  |  ![Not supported](https://i.imgur.com/AxsV1yJ.png "Not supported") |

MicSwitch allows you to mute/unmute your system microphone using a predefined system-wide hotkey which will affect any program that uses microphone (no more heavy breathing during Skype conferences, hooray!)
Also it supports configurable mute/unmute sounds(similar to TeamSpeak/Ventrilo) and a configurable overlay with scaling/transparency support. All these features allow you to seamlessly switch between chat apps and use THE SAME input system with overlay and notifications support.

# Features / Bugfixes priority (click to vote or post feature/bug request)
[![Requests](https://feathub.com/iXab3r/MicSwitch?format=svg)](https://feathub.com/iXab3r/MicSwitch)

# Installation
- You can download the latest version of installer here - [download](https://github.com/iXab3r/MicSwitch/releases/latest).
- After initial installation application will periodically check Github for updates

## Features
- Multiple microphones support (useful for streamers) - ALL microphones in your system could be muted/unmuted by a single key press
- System-wide hotkeys (supports mouse XButtons)
- Always-on-top configurable (scale, transparency) Overlay - could be disable if not needed
- Mute/unmute audio notification (with custom audio files support)
- Customizable tray and overlay icons
- Multiple hotkeys support
- Auto-startup (could be Minimized by default)
- Three Audio modes: Push-to-talk, Push-to-mute and Toggle mute
- Overlay visibility could be linked to microphone state, i.e. it will be shown only when Muted/Unmuted
- Auto-updates via Github

## Media
![UI](https://i.imgur.com/Fz0nTZP.png)

### Overlay with configurable size/opacity
![Overlay with configurable size/opacity](https://i.imgur.com/1Jf1RrH.gif)

### Configurable Audio notification when microphone is muted/unmuted
![Configurable Audio notification when microphone is muted/unmuted](https://i.imgur.com/TmvJizg.png) 

### Customizable overlay/tray icons
![Customizable overlay/tray icons](https://i.imgur.com/Bq0yHnK.png)

### Auto-update via Github
![Auto-update via Github](https://i.imgur.com/O4SIuDy.gif)

## How to build application
* I am extensively using [git-submodules](https://git-scm.com/docs/git-submodule "git-submodules") so you may have to run extra commands (git submodule update) if your git-client does not fully support this tech. I would highly recommend to use [Git Extensions](https://gitextensions.github.io/ "Git Extensions") which is awesome, free and open-source and makes submodules integration seamless
* The main "catch-up-moment" is that you need to run InitSymlinks.cmd before building an application - this is due to the fact that git symlinks are not supported on some older versions of Windows and I am using them to create links to submodules
* I am usually using [Jetbrains Rider](https://www.jetbrains.com/rider/ "Jetbrains Rider") so there MAY be some issues if you are using Microsoft Visual Studio, although I am trying to keep things compatible

### Build from command line
1. git clone https://github.com/iXab3r/MicSwitch.git
2. cd MicSwitch
3. remove submodule DeploymentTools - MicSwitch's installer built and oublished as a part of another project and it's not included into this repository
4. git submodule init 
5. git submodule update --checkout
6. InitSymlinks.cmd
7. dotnet build Sources/MicSwitch

That's it. Portable version will be in **bin** folder. Framework-dependent version

## Linux/MacOS Support ?
Probably not going to happen in the nearest future because in it's core MicSwitch is a WPF application, technology that Microsoft does not want to port at all.
Migrating to something cross-platform like Avalon, Xamarin is an option, but not a cheap one, unfortunately I don't have so much spare time to do it now.

Meanwhile you could take a look at
### MacOS
**MuteKey** (thanks to benpeter for finding it): https://apps.apple.com/us/app/mutekey/id1509590766?mt=12

### Linux
???

## Contacts
- Feel free to contact me via PM in Discord *Xab3r#3780* or [Reddit](https://www.reddit.com/user/Xab3r) 
- [Discord chat](https://discord.gg/BExRm22 "Discord chat")
- [Issues tracker](https://github.com/iXab3r/MicSwitch/issues)
