function InitializeMap() {
    var infowindow = [];
    // Total count of Workplace Locations
    var locationCount = parseInt($("#locationCount").val());
    // Variables arrays
    var lat = [];
    var log = [];
    var locname = [];
    var markers = [];

    // Intialializing first Place
    if (locationCount > 0) {
        // Getting co-ordinates from hidden field
        var coordinates = document.getElementsByClassName("loc")[0].value;
        // Getting Info for Workplace from hidden field
        var contents = document.getElementsByClassName("cor")[0].value;

        var array = coordinates.split("-");
        lat[0] = array[0];   // Latitude
        log[0] = array[1];   // Longitude
        locname[0] = array[2]; // Workplace name
    }
    // Default as well as First Workplace on Map
    var defaultLoc = new google.maps.LatLng(lat[0], log[0]);
    var mapOptions = {
        zoom: 8,
        
        center: defaultLoc,
        height:400
    };
    // Image for Workplace
    var image = 'assets/img/icon-google-map.png'; // marker icon
    // Div in which map will be shown
    var map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
    // Default as well as First Workplace on Map
    var myLatlng = new google.maps.LatLng(lat[0], log[0]);
    markers[0] = new google.maps.Marker({
        position: myLatlng,
        map: map,
        icon: image,
        title: locname[0]
    });

    // Div that will show on click 
    infowindow[0] = new google.maps.InfoWindow({
        content: contents   // Contents for the info window on click 
        , maxWidth: 350
        ,height: 100
    });

    // Onlcick binding 
    google.maps.event.addListener(markers[0], 'click', function () {
        infowindow[0].open(map, markers[0]);
    });

    // Binding for rest of Workplaces
    if (locationCount > 1) {
        for (var i = 1; i < locationCount; i++) {
            coordinates = document.getElementsByClassName("loc")[i].value;
            contents = document.getElementsByClassName("cor")[i].value;
            array = coordinates.split("-");
            lat[i] = array[0];
            log[i] = array[1];
            locname[i] = array[2];

            myLatlng = new google.maps.LatLng(lat[i], log[i]);
            markers[i] = new google.maps.Marker({
                position: myLatlng,
                map: map,
                icon: image,
                title: locname[i]
            });
            // Div that will show on click  | another way 
            markers[i].info = new google.maps.InfoWindow({
                content: contents
               , maxWidth: 350
                , height: 100
            });
            // Click listner | another way 
            (function () {
                var obj = markers[i];
                google.maps.event.addListener(obj, "click", function () { bindInfoWindo(obj); });
            }());
        }
    }
    // Listner handler
    function bindInfoWindo(obj) {
        obj.info.open(map, obj);
    }
}