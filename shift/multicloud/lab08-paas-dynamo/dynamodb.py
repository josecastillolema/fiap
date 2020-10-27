from pprint import pprint
import boto3

dynamodb = boto3.resource('dynamodb', region_name='us-east-1')
table = dynamodb.Table('Alunos')

def scan(dynamodb, table):
    response = table.scan()
    data = response['Items']
    while 'LastEvaluatedKey' in response:
        response = table.scan(ExclusiveStartKey=response['LastEvaluatedKey'])
        data.extend(response['Items'])
    print (data)

def put_aluno(dynamodb, table, rm, mail, nome, tlfne):
    response = table.put_item(
       Item={
            'RM': rm,
            'mail': mail,
            'nome': nome,
            'tfne': tlfne
        }
    )
    return response

if __name__ == '__main__':
    print("\nTestando scan:")
    scan(dynamodb, table)

    resp = put_aluno(dynamodb, table, "RM234472", 'rm234472@fiap.com.br', "Jonas Kahnwald", 11636229987)
    print("\nIserindo aluno:")
    pprint(resp)
