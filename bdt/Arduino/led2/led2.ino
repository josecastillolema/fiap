void setup() {
  pinMode(13, OUTPUT); //porta 13 em output
}

void loop() {
  digitalWrite(13, HIGH); //HIGH = 1 = TRUE
  delay(500);
  digitalWrite(13, LOW); //LOW = 0 = FALSE
  delay(500);
}
