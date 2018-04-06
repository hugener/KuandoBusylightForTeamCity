
# KuandoBusylightForTeamCity
See the TeamCity Build Status with Kuando Busylight

## Examples:

#### KuandoBusylightForTeamcity.exe -h &lt;Host> -b &lt;BuildId> -c "-u &lt;username> -p &lt;password>"

#### KuandoBusylightForTeamcity.exe install -a "-h &lt;Host> -b &lt;BuildId> -c """-u &lt;username> -p &lt;password>""""

#### KuandoBusylightForTeamcity.exe uninstall -a "-h &lt;Host> -b &lt;BuildId>"

## Commandline:
```Help
 Verbs:
  install | Installs Busylight for TeamCity as a Windows Service.
  -a | --arguments | The arguments to use for the installed service. | Required
    -h  | --host-name        | Specifies the team city host name                                                        | Required
    -b  | --build-id         | Specifies the team city buildTypeId                                                      | Required
    -c  | --credentials      | Specifies the credentials to connect to TeamCity                                         | Default: [none]
      -u | --username | Specifies the username | Required
      -p | --password | Specifies the password | Required
    -ri | --refresh-interval | The build status refresh interval within the Range: min: 00:00:00.2000000, max: 00:10:00 | Default: 00:00:01
  -s | --start     | Starts the service after installation.
  uninstall | Uninstalls Busylight for TeamCity as a Windows Service.
  -b | --build-id | Specifies the team city buildTypeId | Required
 Arguments:
  -h  | --host-name        | Specifies the team city host name                                                        | Required
  -b  | --build-id         | Specifies the team city buildTypeId                                                      | Required
  -c  | --credentials      | Specifies the credentials to connect to TeamCity                                         | Default: [none]
    -u | --username | Specifies the username | Required
    -p | --password | Specifies the password | Required
  -ri | --refresh-interval | The build status refresh interval within the Range: min: 00:00:00.2000000, max: 00:10:00 | Default: 00:00:01```
