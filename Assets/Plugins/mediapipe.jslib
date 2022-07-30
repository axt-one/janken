mergeInto(LibraryManager.library, {

  Main: function () {
    const videoElement = document.getElementsByClassName('input_video')[0];

    const jankenHand = document.getElementById('janken-hand');

    class Vector3D {
      constructor(x, y, z) {
        this.x = x;
        this.y = y;
        this.z = z;
      }
      get length() { return Math.sqrt(this.innerP(this)); }

      sub(other) { return new Vector3D(this.x - other.x, this.y - other.y, this.z - other.z); }
      innerP(other) { return this.x * other.x + this.y * other.y + this.z * other.z; }
    }

    function obj2vec(obj) {
      return new Vector3D(obj.x, obj.y, obj.z);
    }

    function isBent(wrist, mcp, dip, tip) {
      const base = obj2vec(mcp).sub(obj2vec(wrist));
      const finger = obj2vec(tip).sub(obj2vec(dip));
      return base.innerP(finger) > 0;
    }

    function onResults(results) {
      if (results.multiHandLandmarks && results.multiHandLandmarks.length) {
        const landmarks = results.multiHandLandmarks[0];
        const isLeft = results.multiHandedness[0].label === 'Right';
        const index = isBent(landmarks[0], landmarks[5], landmarks[7], landmarks[8]);
        const middle = isBent(landmarks[0], landmarks[9], landmarks[11], landmarks[12]);
        const ring = isBent(landmarks[0], landmarks[13], landmarks[15], landmarks[16]);
        const pinky = isBent(landmarks[0], landmarks[17], landmarks[19], landmarks[20]);
        let hand = 0;
        if (index && middle) {
          if (ring && pinky) {
            hand = 3;
          } else {
            hand = 2;
          }
        } else {
          hand = 1;
        }

        const jankenInfo = {
          Hand: hand,
          IsLeft: isLeft,
          PosX: landmarks[0].x,
          PosY: landmarks[0].y,
        }

        jankenHand.value = JSON.stringify(jankenInfo);
      } else {
        const info = JSON.parse(jankenHand.value);
        info.Hand = 0;
        jankenHand.value = JSON.stringify(info);
      }
    }

    const hands = new Hands({
      locateFile: (file) => {
        return `https://cdn.jsdelivr.net/npm/@mediapipe/hands/${file}`;
      }
    });
    hands.setOptions({
      maxNumHands: 1,
      modelComplexity: 1,
      minDetectionConfidence: 0.5,
      minTrackingConfidence: 0.5
    });
    hands.onResults(onResults);

    const camera = new Camera(videoElement, {
      onFrame: async () => {
        await hands.send({ image: videoElement });
      },
      width: 1280,
      height: 720
    });
    camera.start();
  },

  GetHand: function () {
    return Number(document.getElementById('janken-hand').value);
  },

  GetHandInfo: function () {
    const returnStr = document.getElementById('janken-hand').value;
    const bufferSize = lengthBytesUTF8(returnStr) + 1;
    const buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },
});
