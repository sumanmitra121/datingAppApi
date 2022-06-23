-----------------------------------------------------GITHUB-----------------------------------------------
To Clone this project from github : git clone https://github.com/sumanmitra121/datingAppApi.git
----------------------------------------------------------------------------------------------------------
Now If you want to pull or push this project from your local to Github. you need to change username 
and password from credential Manager


 1.If you have a different account on github and if it is still active , Deactivate this  from credential Manager.     
 go to  Control Panel --> Credential Manager --> Windows Credential --> github -> Change UserName Password of the github 
 in which you want to push or pull.

 2.If you want to push your project into github(main): git push -u origin main
 3.If you want to pull project from github (main): git pull origin main
 4.If you want to create branch for your project: git checkout -b <branch-name> (create branch + switch to <branch-name> automatically)
 5.If you want to push your project from branch then you need to check out to your branch first and then you type: 
     1st way: git checkout <branchname>
     2nd way: click on git icon (bottom-left corner) , then enter the branchname  on which you want to switch.
 6.if you want to push your project from branch to branch: git push -u origin <branch-name>
 7.To merge  brancg with main : switch to main ; then git merge <branch-name>;
 -----------------------------------------------------------------------------------------------------------------------------------------
	
Configure IIS In Windows 10 Operating System

In my case, I am using Windows 10 Enterprise edition.
 
Now open the browser and just type the url http://localhost/.
On pressing Enter, it will throw page not found error.
 
Now I am going to search for the IIS manager.
 
Not found!
 
Open Control Panel.

Click Programs.

Under Programs and Features, click Turn Windows features on or off.

A new popup will appear.

Just check the Internet Information Services and its related features.
 
Under Internet Information Services check the following folders:
FTP Server
Web Management Tools
World wide web services
Application development features
Common HTTP Feature
Health and Diagnostics
Performance Feature
Security

Then click OK to complete the installation of new features.
Click Restart now to apply changes to complete the IIS feature installation.

After a successful restart.
 
Now you can see the IIS webserver option on the All Programs menu.
 
Open the browser.
 
Now type url http://localhost.

Internet Information Services (IIS) successfully configured.
