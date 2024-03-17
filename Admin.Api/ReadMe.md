
# Starting up ravenDb locally with Docker

docker run --rm -d -p 8080:8080 -p 38888:38888 -v /var/lib/ravendb/data --name RavenDb-WithData -e RAVEN_Setup_Mode=None -e RAVEN_License_Eula_Accepted=true -e RAVEN_Security_UnsecuredAccessAllowed=PrivateNetwork ravendb/ravendb