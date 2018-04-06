
# KuandoBusylightForTeamCity
See the TeamCity Build Status with Kuando Busylight

## Examples:

#### KuandoBusylightForTeamcity.exe -h "<Host>" -b "&lt;BuildId>" -c "-u &lt;username> -p &lt;password>"

#### KuandoBusylightForTeamcity.exe install -a "-h ""&lt;Host>"" -b ""&lt;BuildId>"" -c """-u &lt;username> -p &lt;password>""""

#### KuandoBusylightForTeamcity.exe uninstall -a "-h ""&lt;Host>"" -b ""&lt;BuildId>""


#### Commandline:
Help  
&nbsp;Verbs:  
&nbsp;&nbsp;&nbsp;install | Installs Busylight for TeamCity as a Windows Service.  
&nbsp;&nbsp;&nbsp;-a | --arguments | The arguments to use for the installed service. | Required  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-h  | --host-name        | Specifies the team city host name                                                         | Required  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-b  | --build-id         | Specifies the team city buildTypeId                                                       | Required  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-c  | --credentials      | Specifies the credentials to connect to TeamCity                                          | Default: [none]  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-u | --username | Specifies the username | Required  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-p | --password | Specifies the password | Required  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-ri | --refresh-interval | The build status refresh interval within the: Range: min: 00:00:00.2000000, max: 00:10:00 | Default: 00:00:01  
&nbsp;&nbsp;uninstall | Uninstalls Busylight for TeamCity as a Windows Service.  
&nbsp;&nbsp;&nbsp;-a | --arguments | The arguments to use for the installed service. | Required  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-h  | --host-name        | Specifies the team city host name                                                         | Required  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-b  | --build-id         | Specifies the team city buildTypeId                                                       | Required  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-c  | --credentials      | Specifies the credentials to connect to TeamCity                                          | Default: [none]  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-u | --username | Specifies the username | Required  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-p | --password | Specifies the password | Required  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-ri | --refresh-interval | The build status refresh interval within the: Range: min: 00:00:00.2000000, max: 00:10:00 | Default: 00:00:01  
&nbsp;Arguments:  
&nbsp;&nbsp;&nbsp;-h  | --host-name        | Specifies the team city host name                                                         | Required  
&nbsp;&nbsp;&nbsp;-b  | --build-id         | Specifies the team city buildTypeId                                                       | Required  
&nbsp;&nbsp;&nbsp;-c  | --credentials      | Specifies the credentials to connect to TeamCity                                          | Default: [none]  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-u | --username | Specifies the username | Required  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-p | --password | Specifies the password | Required  
&nbsp;&nbsp;&nbsp;-ri | --refresh-interval | The build status refresh interval within the: Range: min: 00:00:00.2000000, max: 00:10:00 | Default: 00:00:01  

