#!/bin/bash

wait_time=1s
max_retries=5
password="TenantProductManager&Password1"

# Function to execute the SQL script
function run_sql_script {
  echo "Running SQL script..."
  /opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P $password -i /tmp/init.sql
  return $?
}

# Wait for SQL Server to be available
echo "Data import will begin in $wait_time..."
sleep $wait_time

# Attempts to execute the SQL script
attempt=1
while [ $attempt -le $max_retries ]; do
  run_sql_script
  result=$?

  if [ $result -eq 0 ]; then
    echo "SQL script executed successfully!"
    break
  else
    echo "Failed to execute SQL script. Attempt $attempt of $max_retries. Waiting $wait_time before retrying..."
    sleep $wait_time
  fi

  attempt=$((attempt + 1))
done

if [ $result -ne 0 ]; then
  echo "Failed to execute SQL script after $max_retries attempts. Check the logs for more details."
  exit 1
fi

# Start the specified service (if any)
exec "$@"
