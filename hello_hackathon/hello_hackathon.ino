#include <RGBmatrixPanel.h>

#define CLK  8   // ARDUINO UNO
#define OE   9
#define LAT 10
#define A   A0
#define B   A1
#define C   A2

RGBmatrixPanel matrix(A, B, C, CLK, LAT, OE, true);

int16_t    textX         = matrix.width(),
           textMin       = 60 * -12,
           hue           = 0;

int incomingByte = 0; 
int i = 0;
char str[] = "                                        ";

void setup() {
  matrix.begin();
  matrix.setTextWrap(false); // Allow text to run off right edge
  matrix.setTextSize(2);

  Serial.begin(115200);

  str[0] = '0';
  str[1] = 'O';
  str[2] = 'N';
  
  print(str,10);
  delay(1000);
  resetBuffer();
  
}

void resetBuffer() {
  int upper = 40;
  
   for(int x = 0; x < upper; x++ ) {
      str[x] = ' ';
   }
}

void loop() {
  // send data only when you receive data:
  if (Serial.available() > 0) {

    if(i == 0) {
      resetBuffer();
    }
    
    // read the incoming byte:
    incomingByte = Serial.read();

    if(32 <= incomingByte && incomingByte <= 126) {
  
      char c = incomingByte;


      str[i] = c;
  
      i = i+1;
    }
    
  }
  else {
    if(i > 0) {
      // fill the screen with black
      matrix.fillScreen(matrix.Color333(0, 0, 0));
      

      if(str[0] == '0') {
        print(str, i * -12);  
        delay(5000);
        resetBuffer();
      }
      delay(100);
    }
    i = 0;
  }
  
  delay(1);
  printScroll(str, i*-12);
}



void print(char str[], int size) {
  
  byte i;

  matrix.setCursor(2, 0);  // start at top left, with one pixel of spacing
  matrix.setTextSize(1);   // size 1 == 8 pixels high

  matrix.setTextColor(matrix.Color333(7,0,0));
  for(int x = 1; x <=6; x++){
    matrix.print(str[x]);
  }

  matrix.setCursor(2, 9);  // next line
  matrix.setTextColor(matrix.Color333(0,7,7));
  for(int x = 6; x <= 11; x++){
    matrix.print(str[x]);
  }

  // Update display
  matrix.swapBuffers(false);
}

void printScroll(char str[], int size) {
  

  matrix.setTextSize(2);   

  byte i;

  // Clear background
  matrix.fillScreen(0);

  // Draw big scrolly text on top
  matrix.setTextColor(matrix.ColorHSV(hue, 255, 255, true));
  matrix.setCursor(textX, 1);
  matrix.print(str);

  // Move text left (w/wrap), increase hue
  if((--textX) < textMin) textX = matrix.width();
  hue += 7;
  if(hue >= 1536) hue -= 1536;

  // Update display
  matrix.swapBuffers(false);

  delay(10);
}
