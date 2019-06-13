import paho.mqtt.client as mqtt
import time

# Define the payload to be send
#payload = "{'key':'attribute'}"
payload = "482"

# Define the topic to publish messages
topic = "/pad/intel-galileo/temperatura2"

def send_message(paylaod,topic):
    client.publish(topic, payload, qos=1, retain=True)
    print "Data succesfully published"
        
# The callback for when the client receives a CONNACK response from the server.
def on_connect(client, userdata, flags, rc):
    print("Connected with result code "+str(rc))

def on_message(client, userdata, msg):
    print "msg arrived"

client = mqtt.Client()
client.on_connect = on_connect
client.on_message = on_message
client.connect( "broker.hivemq.com", 1883, 60)

while True:
    send_message(payload,topic)
    time.sleep(10)

client.disconnect()
