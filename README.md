# KuandoBusylightForTeamCity
See the TeamCity Build Status with Kuando Busylight

## Examples:

#### KuandoBusylightForTeamCity.exe -h &lt;Host> -b &lt;BuildTypeId> -c "-u &lt;username> -p &lt;password>"

#### KuandoBusylightForTeamCity.exe install -a "-h &lt;Host> -b &lt;BuildTypeId> -c """-u &lt;username> -p &lt;password>"""" -s

#### KuandoBusylightForTeamCity.exe uninstall -b &lt;BuildTypeId>

## Commandline:
```
Help
 Verbs:
  install | Installs Busylight for TeamCity as a Windows Service.
  -a | --arguments | The arguments to use for the installed service. | Required
    -h  | --host-name        | Specifies the TeamCity host name                                            | Required
    -b  | --build-type-id    | Specifies the TeamCity build type id                                        | Required
    -c  | --credentials      | Specifies the credentials to connect to TeamCity                            | Default: [none]
      -u | --username | Specifies the username | Required
      -p | --password | Specifies the password | Required
    -ri | --refresh-interval | The refresh interval within the Range: min: 00:00:00.2000000, max: 00:10:00 | Default: 00:00:01
  -s | --start     | Starts the service after installation.
  uninstall | Uninstalls Busylight for TeamCity as a Windows Service.
  -b | --build-type-id | Specifies the TeamCity build type id | Required
 Arguments:
  -h  | --host-name        | Specifies the TeamCity host name                                            | Required
  -b  | --build-type-id    | Specifies the TeamCity build type id                                        | Required
  -c  | --credentials      | Specifies the credentials to connect to TeamCity                            | Default: [none]
    -u | --username | Specifies the username | Required
    -p | --password | Specifies the password | Required
  -ri | --refresh-interval | The refresh interval within the Range: min: 00:00:00.2000000, max: 00:10:00 | Default: 00:00:01
  ```
