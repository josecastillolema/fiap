from flask import Flask
app = Flask(__name__)

@app.route("/")
def hello():
    return "<h1>Hola FIAP!</h1>\nMobile Development! o/"

