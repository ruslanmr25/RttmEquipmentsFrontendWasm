


function myselectstart() {
     document.addEventListener("DOMContentLoaded", function (e) {
          const myselect1 = document.querySelector('#myselect'); console.log(myselect1);
          document.addEventListener("DOMContentLoaded", function () {
               const body = document.querySelector("body");

               body.addEventListener('click', () => {
                    console.log("I'm click");
               });

               const select = body.querySelector("#myselect");
               console.log(select); // endi bu null bo'lmaydi, agar ID to'g'ri bo'lsa
          });

     });

} 