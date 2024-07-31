#!/bin/bash
wait_time=15

echo "Waiting $wait_time seconds before starting the service..."
sleep $wait_time

echo "Starting the specified service..."
exec "$@"
