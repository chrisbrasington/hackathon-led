#include <RGBmatrixPanel.h>

#define CLK  8   // ARDUINO UNO
#define OE   9
#define LAT 10
#define A   A0
#define B   A1
#define C   A2

RGBmatrixPanel matrix(A, B, C, CLK, LAT, OE, false);

int16_t    textX         = matrix.width(),
           textMin       = 60 * -12,
           hue           = 0;

void setup() {
  matrix.begin();
  matrix.setTextWrap(false); // Allow text to run off right edge
  matrix.setTextSize(2);

  Serial.begin(115200);
  Serial.println("DONE");
  
}

int incomingByte = 0; 
int i = 0;
char str[] = "                                        ";

void resetBuffer() {
   for(int x = 0; x < 40; x++ ) {
      str[x] = ' ';
   }
}

void loop() {
  
  //print(str, sizeof(str) * -12);
  //delay(10);

  // send data only when you receive data:
  if (Serial.available() > 0) {
    // read the incoming byte:
    incomingByte = Serial.read();

    // say what you got:
    Serial.print("I received: ");
    char c = incomingByte;
    Serial.println(c);

    str[i] = c;

    i = i+1;
    
  }
  else {
    if(i > 0) {
      // fill the screen with black
      matrix.fillScreen(matrix.Color333(0, 0, 0));
      
      Serial.print("RESULT: ");
      Serial.println(str);
      Serial.println(i);
      
      print(str, i * -12);
      delay(100);
      
      resetBuffer();
    }
    i = 0;
  }
}



void print(char str[], int size) {
  textMin       = size;
  byte i;

  Serial.println("PRINT!!!!");
  Serial.println(str);

  matrix.setCursor(2, 0);  // start at top left, with one pixel of spacing
  matrix.setTextSize(1);   // size 1 == 8 pixels high

  // print each letter with a rainbow color
  //matrix.print("12345");
  matrix.setTextColor(matrix.Color333(7,0,0));
  for(int x = 0; x <=5; x++){
    matrix.print(str[x]);
  }

  matrix.setCursor(2, 9);  // next line
  matrix.setTextColor(matrix.Color333(0,7,7));
  for(int x = 5; x <= 10; x++){
    matrix.print(str[x]);
  }
}
