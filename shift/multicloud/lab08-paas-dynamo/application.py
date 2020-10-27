from flask import Flask
from pprint import pprint
import boto3
import json

application = Flask(__name__)

dynamodb = boto3.resource('dynamodb', region_name='us-east-1')
table = dynamodb.Table('Alunos')

def scan(dynamodb, table):
    response = table.scan()
    data = response['Items']
    while 'LastEvaluatedKey' in response:
        response = table.scan(ExclusiveStartKey=response['LastEvaluatedKey'])
        data.extend(response['Items'])
    return (str(data))

@application.route("/")
def hello():
    head = '<h1>Alunos</h1>'
    return(head + scan(dynamodb, table))


if __name__ == '__main__':
    application.run(host='0.0.0.0', debug=True)
