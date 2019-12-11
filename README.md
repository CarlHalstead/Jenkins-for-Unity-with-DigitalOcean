# Jenkins for Unity with DigitalOcean

## Contents

* [What is...?](#what-is)
  - [DigitalOcean](#digitalocean)
  - [Jenkins](#jenkins)
  - [Unity](#unity)
  
* [Questions](#questions)
   - [I have an error!](#i-have-an-error)
   - [I have a suggestion!](#i-have-a-suggestion)
   - [I use LFS, will this work with LFS?](#i-use-lfs-will-this-work-with-lfs)

* [Prerequisites](#prerequisites)
   - [Required](#required)
   - [Recommended](#recommended)
   - [Optional](#optional)
   
* [Getting Started](#getting-started)
   - [Unity Installation](#unity-installation)
   - [Jenkins Setup](#jenkins-setup)
   
* [Conclusion](#conclusion)

## What is...?

### DigitalOcean
DigitalOcean is a cloud platform that offers a huge range of services from the more simple storage and Databases to the more advanced 'Droplet' which is your very own Virtual Machine (VM) in the cloud. For the sake of this tutorial, we will be using a Droplet so that we can install our own software onto it.

### Jenkins
Jenkins is a task automation tool which can be used to tackle an enourmous tange of tasks. Really, the only limit is your imagination. It allows you to create automatically download source control repositores and manipulate them through either Command Prompt or the Terminal depending on your Operating System. For this tutorial we will be using a Shell script to launch Unity with out GitHub project and automatically build and upload the finished build so that it can be tested with ease.

### Unity
Unity very simply, is a game engine. It is capable of both 2D and 3D games and chances are you have probably played a game made with the engine. If for whatever reason you have not tried it out yet, I would seriously recommend giving it a go and seeing what you can create!

## Questions

### I have an error!
If you encounter any problems while following this guide, create an 'Issue' on this repository and I will try and respond as soon as posssible. With any luck it will be something simple that we can sort in a few minutes.

### I have a suggestion!
Suggestion are always welcome! Please create an 'Issue' clearly stating what you think could be improved.

### I use LFS, will this work with LFS?
This post does not currently go over Git LFS but I believe there is a plugin within Jenkins that you could install and use to ensure the checkout is done with LFS.

## Prerequisites

#### Required
* [DigitalOcean account ($50 free credit with my referral](https://m.do.co/c/7d4a120f8b9b))
* DigitalOcean Droplet (This is a Virtual Machine in the cloud which you can use as you wish. [I have found that you need at least 2GB to compile most Unity projects](https://forum.unity.com/threads/jenkins-ubuntu-executemethod-class-unitybuild-could-not-be-found.764132/). If you created a Droplet with 1GB you can simply shut down the server, [resize it (CPU and RAM Only)](https://www.digitalocean.com/docs/droplets/how-to/resize/) and restart the server.)

1. [Initial Server Setup with Ubuntu 18.04](https://www.digitalocean.com/community/tutorials/initial-server-setup-with-ubuntu-18-04)
2. [How To Install Jenkins on Ubuntu 18.04](https://www.digitalocean.com/community/tutorials/how-to-install-jenkins-on-ubuntu-18-04)
3. [My UnityBuild.cs located within your project. It should be contained within an Editor folder which itself resides within the Assets folder. Assets -> Editor -> UnityBuild.cs for example.](Unity/Editor/UnityBuild.cs)

#### Recommended
For those who are not quite as comfortable in the Linux environment just yet [I also recommend installing a VNC client](https://www.digitalocean.com/community/tutorials/how-to-install-and-configure-vnc-on-ubuntu-18-04) which will allow you to stream the video feed to your computer and interact with a GUI for the parts that you do not know off the top of your head in the terminal. 

#### Optional
The following optional prerequisits are for if you would like your Jenkins installation available through a normal domain such as a .com or .co.uk instead of your servers IP address. I personally recommend these steps so you do not have to keep checking your servers IP address and considering you can get a free domain for a year, it does not cost you anything.  

* Registered domain (You can get one for free from Freenom)

1. [How To Install Nginx on Ubuntu 18.04](https://www.digitalocean.com/community/tutorials/how-to-install-nginx-on-ubuntu-18-04)
2. [How To Secure Nginx with Let's Encrypt on Ubuntu 18.04](https://www.digitalocean.com/community/tutorials/how-to-secure-nginx-with-let-s-encrypt-on-ubuntu-18-04)
3. [How To Configure Jenkins with SSL Using an Nginx Reverse Proxy on Ubuntu 18.04](https://www.digitalocean.com/community/tutorials/how-to-configure-jenkins-with-ssl-using-an-nginx-reverse-proxy-on-ubuntu-18-04)

## Getting Started

### Unity Installation

Now that your sever is up and running I will be assuming that you have an open SSL connection to your server to run any commands as well as VNC connected.

#### NOTE: As far as I am aware, you will need a VNC client to be able to use the Unity Hub. I am not aware of any command line arguments being implemented!

1. [Download the latest version of the Unity Hub](https://forum.unity.com/threads/unity-hub-v-1-0-0-is-now-available.555547/), either through the link provided or by running ``` wget https://public-cdn.cloud.unity3d.com/hub/prod/UnityHub.AppImage ``` from the terminal. (You may need to give it executable permissions. Either by right-clicking it through your VNC client and going to Properties -> Permissions -> 'Allow executing file as program' or by executing the following command ```sudo chmod +x FileNameHere.ExtensionHere```)
2. When executable, run Unity Hub (Either by double-clicking the .AppImage or by running ``` ./UnityHub.AppImage ```) and sign in. This will give you a Unity license to use when building your projects.
3. Go to /opt/ and create a folder called 'unity' with the following command ``` sudo mkdir -m 777 "unity" ```
4. Return to the Unity Hub and go to the Settings cog in the top-right. Change the 'Unity Editors Folder' to /opt/unity and click save.
![Unity Hub editor location](https://i.imgur.com/w2aoavn.png)
5. Install the version(s) of Unity you will use to build your projects. (Note: The Unity Hub does not show all available versions, there are more [here](https://forum.unity.com/threads/unity-on-linux-release-notes-and-known-issues.350256/)). Just make sure to install them into the same location as those from ther Unity Hub!

After successful installation of the Unity editor(s), you should have a folder structure that look like so.
![Unity installation folder structure](https://i.imgur.com/ajwLyn6.png)

### Jenkins Setup

Now that you have your server setup and Unity installed we are now ready to jump into Jenkins! (Note: For simplicity we well assume that your repository is hosted on GitHub. I may update this to include other hosts but for now, just GitHub)

1. Go to Jenkins, whether this be your servers IP address as seen in DigitalOcean or a domain as recommended in the optional prerequisits steps.
2. Create a new project (For sake of simplicity I recommend having no spaces in the name)
![Location of the 'New Item' button](https://i.imgur.com/j16pfT7.png)
3. Give your project a name and choose 'Freestyle Project' for the type, click Next.
![Jenkins project name and type](https://i.imgur.com/cMJpmWm.png)
4. Tick the GitHub project box as shown and enter the URL of your project.
![Jenkins GitHub URL](https://i.imgur.com/2iAlQIX.png)
5. Next, under 'Source Code Management' select 'Git' and where it says 'Repository URL' enter the same URL you entered previously but remove the last slash and add .git as shown below.
![Jenkins GitHub URL Cont.](https://i.imgur.com/dTyjx3a.png)
6. Go to the [following GitHub link](https://github.com/settings/tokens) and click 'Generate new token' (You will be prompted for your password). Under 'Note' give this token a name such as 'Jenkins', something simple to refer to your server and tick the top box called simply 'repo' and click 'Generate Token'. This will allow the server to build private repositories too! (Note: Make sure to copy your new token!)
![GitHub access token creation](https://i.imgur.com/QYU37yi.png)
![GitHub access token creation location](https://i.imgur.com/sy7CmMf.png)
7. Next we will add this token to your Jenkins credentials. Under 'Credentials' click 'Add' -> 'Jenkins'.
![Jenkins - adding credentials](https://i.imgur.com/tVvfmYy.png)
8. In the username field, input your GitHub email. For your password, you will paste the token you created just a moment ago. If you forgot to copy it, you will need to create a new one. Give it a sensible description. Click 'Add'
![Jenkins credential creation](https://i.imgur.com/VyNPTSU.png)
9. In the 'Credentials' dropdown select your newly created credentials.
![jenkins credential selection](https://i.imgur.com/zJ5XPoE.png)
10. In the 'Build Trigger' section we will set it up so that GitHub pings your server when a new commit is made. You could change it so that it checks for updates at regular intervals however this may lead to a delay to your build after commiting. Tick 'GitHub hook trigger for GITScm polling'.
![Jenkins build after committing](https://i.imgur.com/z4E5b1S.png)
11. Go to your GitHub repository. Go to 'Settings' -> 'Webhooks' -> 'Add Webhook'
![GitHub webhooks](https://i.imgur.com/2mxdchJ.png)
12. For the 'Payload URL' input either the IP address of your server or your domain with '/github-webhook/' appended to the end. Make sure SSL verification is enabled and select 'Just the push event'. Create the webhook.
![GitHub webhook setup](https://i.imgur.com/ybeRznP.png)
13. Now for the meat of the process, how to actually build your application. Here, I will show you what I am personally using and how to edit that to fit your own needs. Click 'Add build step' -> 'Execute shell'. My script is as follows, feel free to paste it in:

```
# Paths for reference for Unity installs and Unity licenses (.ulf files)
# Each installatuion of Unity should be contained within a folder where the root name is
# simply its version e.g. 2019.2.3f1 or 2019.3.1f1
UNITY_PATH="/opt/unity"

# Within this next block of commands we accomplish a single
# task but in a modular fashion!
# 1. Find a file called ProjectVersion.txt within a folder called ProjectSettings. (This can be anywhere in the workspace)
# 2. Read said file
# 3. Using a regular expression, extract the version number used for the project
# 4. Trim whitespace around the version
cd "$WORKSPACE"
UNITY_VERSION_FILE_PATH="$(find . | grep ProjectSettings/ProjectVersion.txt)"
UNITY_VERSION_CONTENTS=$(cat "$UNITY_VERSION_FILE_PATH")
UNITY_VERSION="$(echo $UNITY_VERSION_CONTENTS | grep -oP '(?<=m_EditorVersion: ).*?(?=m_EditorVersionWithRevision:)')"
UNITY_VERSION="$(echo $UNITY_VERSION | xargs)"

# Make a directory within the workspace where the build will be deposited.
# Anyone can write to this folder.
mkdir -m 777 -p "$WORKSPACE/_Final_"

# Launch Unity with the desired project and parameters.
"$UNITY_PATH/$UNITY_VERSION/Editor/Unity" -projectPath "$WORKSPACE/" -executeMethod UnityBuild.BuildPlatforms -buildPath "$WORKSPACE/_Final_/" -windows64 -batchmode -nographics -quit
```

This script should work by just being pasted in. The only thing you need to change is the UNITY_PATH variable. This should be the folder with your version of Unity installed. It will contain folder simply called the version of the editor inside. 2019.2.3f1 for example. If you have been following along, this should be /opt/unity as shown in the script.

Note: By default, this script will build for Windows 64bit as denoted by the -windows64 command line argument. You can see the other available platforms [here](Unity/Editor/UnityBuild.cs#L16) and simply append them to the arguments. For example if you wanted to build for Linux 64bit, Mac OS and WebGL, it would read:

```
"$UNITY_PATH/$UNITY_VERSION/Editor/Unity" -projectPath "$WORKSPACE/" -executeMethod UnityBuild.BuildPlatforms -buildPath "$WORKSPACE/_Final_/" -linux64 -macos -webgl -batchmode -nographics -quit
```

For more information on the command line arguments I am using to launch Unity, [here is the official Untiy documentation page.](https://docs.unity3d.com/Manual/CommandLineArguments.html)
14. Finally, bringing this tutorial to an end, there are 3 post-build actions I use (Although you may not want to use one or another for various reasons) and these are:

* 'Archive the artifacts', where the 'File to archive' field contains '_Final_/&ast;&ast;/&ast;' which is where we build our project. 
![Jenkins archiving artifacts](https://i.imgur.com/nZbyCba.png)

* The second post-build action I use called 'Set GitHub commit status'. What this does is simply let GitHub know whether a build has passed or failed and shows a tick or cross like so with a link to the build.
![GitHub commit status](https://i.imgur.com/NZeMeSG.png)

The only setting to change under 'Set GitHub commit status' is called 'Status result' which needs to be changed to 'One of default messages and statuses'.
![Jenkins set GitHub commit status](https://i.imgur.com/F400URW.png)

* Lastly, 'Delete workspace when build is done' will just wipe everything when the build has finished. Pretty simple.

## Conclusion

Now, you should be able to click 'Build Now' and watch it build through the console in Jenkins.
![Jenkins build now](https://i.imgur.com/SDOzz8q.png)

With any luck, you will have an error free first build like I did with this tutorial as well as artifacts that yoiu can download and try out!
![Jenkins successful build with artifacts](https://i.imgur.com/xlpapaq.png)

### Now go! Go and make awesome things!
