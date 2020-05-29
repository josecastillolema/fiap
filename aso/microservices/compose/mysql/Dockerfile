FROM mysql

MAINTAINER Jose Castillo <profjose.lema@fiap.com.br>

#ENV MYSQL_USER=root \
#    MYSQL_DATABASE=fiapdb \
#    MYSQL_ROOT_PASSWORD=senhaFiap

ADD ./aso.sql /docker-entrypoint-initdb.d
