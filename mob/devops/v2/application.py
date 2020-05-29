import pyodbc
from flask import Flask
app = Flask(__name__)

server = '<meuserver>.database.windows.net'
database = '<meudatabase>'
username = '<meuuser>'
password = '<mihasenha>'
driver = '{ODBC Driver 17 for SQL Server}'

@app.route("/")
def hello():
    cnxn = pyodbc.connect('DRIVER='+driver+';SERVER='+server+';PORT=1433;DATABASE='+database+';UID='+username+';PWD='+ password)
    cursor = cnxn.cursor()
    cursor.execute("SELECT TOP 20 pc.Name as CategoryName, p.name as ProductName FROM [SalesLT].[ProductCategory] pc JOIN [SalesLT].[Product] p ON pc.productcategoryid = p.productcategoryid")
    row = cursor.fetchone()
    s = ''
    while row:
       s = s + "<tr><td>" + (str(row[0]) + " </td><td>" + str(row[1])) +'</td></tr>\n'
       row = cursor.fetchone()
    return "<h1>Hola FIAP!</h1>\n<table>"+s+"\n</table>"

