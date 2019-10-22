# Jenkins for Unity with DigitalOcean

## Prerequisites

#### Required
* DigitalOcean account ($50 free credit with my [referral](https://m.do.co/c/7d4a120f8b9b))
* DigitalOcean Droplet (This is a Virtual Machine in the cloud which you can use as you wish. [I have found that you need at least 2GB to compile most Unity projects](https://forum.unity.com/threads/jenkins-ubuntu-executemethod-class-unitybuild-could-not-be-found.764132/). If you created a Droplet with 1GB you can simply shut down the server, [resize it (CPU and RAM Only)](https://www.digitalocean.com/docs/droplets/how-to/resize/) and restart the server.)

1. [Initial Server Setup with Ubuntu 18.04](https://www.digitalocean.com/community/tutorials/initial-server-setup-with-ubuntu-18-04)
2. [How To Install Jenkins on Ubuntu 18.04](https://www.digitalocean.com/community/tutorials/how-to-install-jenkins-on-ubuntu-18-04)

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

Now that your sever is up and running I will be assuming that you have an open SSL connection to your server with your own user so you can run commands in the terminal.

1. [Download the latest version of the Unity Hub](https://forum.unity.com/threads/unity-hub-v-1-0-0-is-now-available.555547/). You may need to give it executable permissions. Either by right-clicking it through your VNC client and going to Properties -> Permissions -> Allow executing file as program or by 'cd'ing to the folder through the terminal and running ```sudo chmod +x UnityHubFileName```
2. When executable, run Unity Hub (The file you just downloaded) and sign in. This will give you a Unity license to use when building your projects.
3. Install the version(s) of Unity you will use to build your projects. (Note: The Unity Hub does not show all available versions, there are more [here](https://forum.unity.com/threads/unity-on-linux-release-notes-and-known-issues.350256/))

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
