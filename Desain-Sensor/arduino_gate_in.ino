const int buttonPin = 7;
const int loopPin = 4;
const int gatePin = 2;
const int ledPin = 13;
const int relayPin = 12;

int buttonState = 0;
int loopState = 0;
int gateState = 0;

int prevButtonState = HIGH;
int prevLoopState = HIGH;
int prevGateState = HIGH;

char Car = 'T';
char Print = 'T';

void setup() {
  pinMode(gatePin, INPUT_PULLUP);
  pinMode(buttonPin, INPUT_PULLUP);
  pinMode(loopPin, INPUT_PULLUP);
  pinMode(ledPin, OUTPUT);
  pinMode(relayPin, OUTPUT);
  digitalWrite(loopPin, HIGH);
  digitalWrite(gatePin,HIGH);
  digitalWrite(buttonPin, HIGH);
  digitalWrite(relayPin, LOW);
  Serial.begin(9600);
  Car = 'T';
  Print = 'Y';
}

void loop() {
  loopState = digitalRead(loopPin);
  if ((loopState != prevLoopState) && (loopState == LOW) && (Car == 'T')) {
    while(Serial.available()){
     Serial.println("S");
    }
   Car = 'Y';
  }

  if (loopState == LOW) {  
   Car = 'Y';
  }
  prevLoopState = loopState;
 
  if ((Car == 'Y') && (Print == 'Y')) {
   buttonState = digitalRead(buttonPin);
   if ((buttonState != prevButtonState) && (buttonState == LOW)) {     
    while(Serial.available()){
        Serial.println("P");
    }
       digitalWrite(relayPin, HIGH);
       digitalWrite(ledPin, HIGH);
       Print = 'T';
       delay(100);
       digitalWrite(relayPin, LOW);  
     }
    prevButtonState = buttonState;
  }

  gateState = digitalRead(gatePin);
  if ((gateState != prevGateState) && (gateState == LOW)) {
   Print = 'Y';
   Car = 'T';
   digitalWrite(ledPin, LOW);
  }
   if (gateState == LOW) {
   Print = 'Y';
   Car = 'T';
   digitalWrite(ledPin, LOW);
   delay(500);
  }
  prevGateState = gateState;
}
