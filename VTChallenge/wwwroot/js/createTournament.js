$(document).ready(function () {
  var select = $("#selectRank");

  var rutaImagenes = "../imgs/rangos";

  // creamos un array para almacenar las rutas de las imágenes
  var imagenes = [];

  // hacemos una petición AJAX para obtener las imágenes
  $.ajax({
    url: rutaImagenes,
    success: function (data) {
      // buscamos todas las etiquetas <a> que contengan el nombre de archivo de una imagen
      $(data)
        .find('a:contains(".png")')
        .each(function () {
          // obtenemos la ruta completa de la imagen y la agregamos al array
          var rutaCompleta = $(this).attr("href");
          var partes = rutaCompleta.split("/");
          var rutaRelativa = partes.slice(partes.indexOf("imgs")).join("/");

          imagenes.push("../" + rutaRelativa);
        });

      // generamos las opciones
      for (var i = 0; i < imagenes.length; i++) {
        // creamos la opción y le establecemos el valor y el texto
        var cadena = imagenes[i];

        // obtenemos el nombre del archivo sin extensión
        var nombreArchivo = cadena.substring(
          cadena.lastIndexOf("/") + 1,
          cadena.lastIndexOf(".")
        );
        // extraemos el nombre del rango y lo formateamos
        var rangoNombre = nombreArchivo
          .substring(0, nombreArchivo.lastIndexOf("_"))
          .replace(/_/g, " ");

        var opcion = $("<option>", {
          value: imagenes[i],
          text: rangoNombre,
        });

        // agregamos la opción al select
        select.append(opcion);
      }

      $("#imagenRank").html(
        '<img src="' + imagenes[0] + '" width="60px" height="60px">'
      );
    },
  });

  // cuando se cambie el select
  select.change(function () {
    // obtenemos el valor seleccionado
    var valorSeleccionado = $(this).val();

    // cargamos la imagen asociada
    $("#imagenRank").html(
      '<img src="' + valorSeleccionado + '" width="60px" height="60px">'
    );
  });

  $("#jugadores").change(function () {
    generateRounds($(this).val());
  });
});

function generateRounds(teams) {
  var rounds = $("#rondas");
  var nombresRondas = [
    "Octavos de Final",
    "Cuartos de Final",
    "Semifinal",
    "Final",
  ];
  var numEquipos = teams / 5;

  switch (numEquipos) {
    case 2:
      numRondas = 1;
      break;
    case 4:
      numRondas = 2;
      break;
    case 8:
      numRondas = 3;
      break;
    case 16:
      numRondas = 4;
      break;
    default:
      console.log("Número de equipos no válido");
  }
  rounds.empty();
  // pintar las rondas en la página
  for (var i = 0; i < numRondas; i++) {
    rounds.append(`<div class="row mt-4">
                            <div class="col-12 col-sm-6">
                                <input
                                    class="multisteps-form__input form-control"
                                    type="text"
                                    value="${
                                      nombresRondas[i + (4 - numRondas)]
                                    }"
                                    name="nameRound"
                                    readonly
                                />
                            </div>
                            <div
                                class="col-12 col-sm-6 mt-4 mt-sm-0"
                            >
                                <input
                                    class="multisteps-form__input form-control"
                                    type="date"
                                    name="dateRound"
                                    min=""
                                />
                            </div>
                        </div>`);
  }
  rounds.append(`<div class="button-row d-flex justify-content-between mt-4">
                        <div class="button-borders">
                            <button
                                class="secondary-button secondary-background js-btn-prev"
                                type="button"
                                title="Prev"
                            >
                                Prev
                            </button>
                        </div>
                        <div class="button-borders">
                            <button
                                class="secondary-button secondary-background js-btn-next"
                                type="button"
                                title="Next"
                            >
                                Next
                            </button>
                        </div>
                    </div>`);
  generateClashes(numEquipos, nombresRondas[0 + (4 - numRondas)]);
  //generatePartidosRes(numEquipos);
}

function generateClashes(teams, nombreRonda) {
  var clasification = $("#clasificacion");
  clasification.empty();
  // pintar los equipos en la página
  var html = `<h1 class="terciary-title-valo mt-4">Partidos de ${nombreRonda}</h1>`;
  for (var i = 0; i < teams - 1; i += 2) {
    var equipo1 = `Team ${i + 1}`;
    var equipo2 = `Team ${i + 2}`;
    html += `
        <div class="box-container">
                <div
                    draggable="true"
                    class="box"
                >
                    ${equipo1}
                </div>
                <span class="box-text">VS</span>
                <div
                    draggable="true"
                    class="box"
                >
                    ${equipo2}
                </div>
                <input
                    class="multisteps-form__input form-control"
                    type="time"
                    name="time-match"
                    min=""
                />
        </div>
        `;
  }
  html += `<div class="row mt-4" id="partidos"></div>`;
  clasification.append(html);
  clasification.append(`<div class="button-row d-flex justify-content-between mt-4">
                            <div class="button-borders">
                                <button
                                    class="secondary-button secondary-background js-btn-prev"
                                    type="button"
                                    title="Prev"
                                >
                                    Prev
                                </button>
                            </div>
                            <div class="button-borders">
                                <button
                                    class="secondary-button secondary-background js-btn-next"
                                    type="button"
                                    title="Next"
                                >
                                    Next
                                </button>
                            </div>
                        </div>`);
  dragDrop();
}

function dragDrop() {
  function handleDragStart(e) {
    this.style.opacity = "0.4";

    dragSrcEl = this;

    e.dataTransfer.effectAllowed = "move";
    e.dataTransfer.setData("text/html", this.innerHTML);
  }

  function handleDragEnd(e) {
    this.style.opacity = "1";

    items.forEach(function (item) {
      item.classList.remove("over");
    });
  }

  function handleDragOver(e) {
    if (e.preventDefault) {
      e.preventDefault();
    }

    return false;
  }

  function handleDragEnter(e) {
    this.classList.add("over");
  }

  function handleDragLeave(e) {
    this.classList.remove("over");
  }

  function handleDrop(e) {
    e.stopPropagation();

    if (dragSrcEl !== this) {
      dragSrcEl.innerHTML = this.innerHTML;
      this.innerHTML = e.dataTransfer.getData("text/html");
    }

    return false;
  }

  let items = document.querySelectorAll(".box");
  items.forEach(function (item) {
    item.addEventListener("dragstart", handleDragStart);
    item.addEventListener("dragover", handleDragOver);
    item.addEventListener("dragenter", handleDragEnter);
    item.addEventListener("dragleave", handleDragLeave);
    item.addEventListener("dragend", handleDragEnd);
    item.addEventListener("drop", handleDrop);
  });
}
