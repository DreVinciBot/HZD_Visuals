from flask import Flask, render_template

app = Flask(__name__)


@app.route('/')
def unitygame():
    return render_template("index.html")
    # return 'Hello, World! test 2'