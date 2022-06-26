FROM mysql:8.0.26
ARG file=health-medicines.sql
COPY ./$file /docker-entrypoint-initdb.d/$file
