#!/usr/bin/env sh

migrationScript="$1"

# psql exits with 2 when the connection fails and 1 when the command fails so
# we loop until we get a non-2 exit code then proceed if it was 0.

while :; do
    psql --command 'SELECT 1' 1>/dev/null
    lastExitCode=$?
    if [[ "$lastExitCode" != "2" ]]; then
        break
    fi
    echo "[$(date '+%Y-%m-%dT%H:%M:%S')] waiting for ${PGHOST}:${PGPORT}..."
    sleep 1s
done

if [[ "$lastExitCode" = "0" ]]; then
    psql -v ON_ERROR_STOP=1 --file "$migrationScript"
    exit $?
fi

echo "wait loop exited with non-zero exit code '$lastExitCode'" 1>&2
