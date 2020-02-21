// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function () {
    //console.log("cargada el archivo site.js")

   // llamadaAjaxsGrafico = function (params, AreaGrafica) {
    llamadaAjaxsGrafico = function (programa) {
        //data: params,

        $.ajax({
            type: "POST",
            async: false,

            url: 'ProgramasPrestacions/Flechas',
            data: { programa },
               
            success: function (msg) {
                estado = msg;
                console.log("Anduvo ok", msg[0].GrupoPrograma)
                console.log(JSON.stringify(msg));
            }
            //},
            //error: function (msg) {
            //    console.log(msg)
            //}
        });






    }
    llamadaAjaxsGrafico('400');
        /*
            $.ajax({
                type: "POST",
                async: false,
                //url: "/Home/leerCambiosEstados/",
                url: '@Url.Action("Flechas","Programa")',
                data: params,//'{ "grupoparam": "117" }',

                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    estado = msg;
                },
                error: function (msg) {
                    $("#dvAlerta > span").text("Error al llenar el combo");
                }
            });

        $.ajax({
            type: "POST",
            async: false,
            url: '@Url.Action("Burbujas","Programa")',
            data: params,//'{ "grupoparam": "117" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg2) {

                var nodes = new vis.DataSet(estado);//flechas

                // create an array with edges
                var edges = new vis.DataSet(msg2);

                // create a network
                var container = AreaGrafica;// document.getElementById('mynetwork2');
                var data = {
                    nodes: nodes,
                    edges: edges
                };

                //var options = {};
                var options = {
                    layout: {
                        randomSeed: 120,
                        improvedLayout: true,
                        hierarchical: {
                            enabled: false,
                            levelSeparation: 250,
                            nodeSpacing: 5,
                            treeSpacing: 200,
                            blockShifting: true,
                            edgeMinimization: true,
                            parentCentralization: true,
                            direction: 'RL',        // UD, DU, LR, RL
                            sortMethod: 'hubsize'   // hubsize, directed
                        }
                    },
                    edges: {
                        arrows: {
                            to: { enabled: true, scaleFactor: 1, type: 'arrow' },
                            middle: { enabled: false, scaleFactor: 1, type: 'arrow' },
                            from: { enabled: true, scaleFactor: 25, type: 'arrow' }
                        },
                        arrowStrikethrough: true,
                        physics: false,
                        font: {
                            color: '#343434',
                            size: 8, // px
                            face: 'arial',
                            background: 'none',
                            //strokeWidth: 5, // px
                            //strokeColor: '#f44ff',
                            align: 'horizontal',
                            multi: false,
                        },
                        scaling: {
                            min: 1,
                            max: 5,
                            label: {
                                enabled: true,
                                min: 1,
                                max: 30,
                                maxVisible: 50,
                                drawThreshold: 8
                            },
                        }
                    }
                }
                //cuando se toca una burbuja -nodos¡
                var network = new vis.Network(container, data, options);
                 network.on("click", function (params) {

                    $.ajax({
                        type: "POST",
                        async: false,
                        url: '@Url.Action("Graficar2","Programa")',
                        data: "{}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg2) {

                            var nodes = new vis.DataSet(estado);

                            // create an array with edges
                            var edges = new vis.DataSet(msg2);

                            // create a network
                            var container = document.getElementById('mynetwork');
                            var data = {
                                nodes: nodes,
                                edges: edges
                            };

                        }
                    }
                    );

                    params.event = "[original event]";
                    document.getElementById('eventSpan').innerHTML = '<h2>Click event:</h2>' + JSON.stringify(params, null, 4);
                    //console.log('click event, getNodeAt returns: ' + this.getNodeAt(params.pointer.DOM));

                });

                mynetwork

            },
            error: function (msg) {
                $("#dvAlerta > span").text("Error al llenar el combo");
            }
        });*@
       
                     }
});

        

       


        } */

})()