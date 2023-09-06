var geocoder;
var bounds = new Array();
var map = new Array();

function initOptionsMap() {
    return {
        zoom: 1,
        center: new google.maps.LatLng(48, 2),
        mapTypeControl: true,
        mapTypeControlOptions: {
            style: google.maps.MapTypeControlStyle.DROPDOWN_MENU
        },
        scaleControl: false,
        disableDefaultUI: true,
        zoomControl: true,
        scrollwheel: false,
        zoomControlOptions: {
            style: google.maps.ZoomControlStyle.SMALL
        },
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }
}

function initialize(mapName, zoom) {
    geocoder = new google.maps.Geocoder();
    var myOptions = initOptionsMap();
    if (typeof zoom != 'undefined') {
        myOptions['zoom'] = zoom;
    }
    map[''+mapName] = new google.maps.Map(document.getElementById("map_canvas_gmaps"+mapName), myOptions);
}

function zoomToBounds(boundsFromGeocoder, mapName){
    if(bounds[''+mapName] == undefined) {
        bounds[''+mapName] =  new google.maps.LatLngBounds();
    }
    bounds[''+mapName].extend(boundsFromGeocoder);
    map[''+mapName].fitBounds(bounds[''+mapName]);
}

/**
 * Adding marker on the google map by coordinates
 * Format data addMarcers((object) map, (string) 'mapName', (json) {name:'Marker name', link:'http://www.site.com/page.html', address:'City, Address 23, 4577', 'lat':'0.454', 'lng':'4.574'})
 */
function addMarkerOnMap(gmap, mapName, marker) {
    var name = (marker.hasOwnProperty('link')) ? '<a href="'+marker.link+'">'+marker.name+'</a>' : marker.name;

    // The place where loc contains geocoded coordinates
    var latLng    = new google.maps.LatLng(parseFloat(marker.lat), parseFloat(marker.lng));
    var newMarker = new google.maps.Marker({
        map: gmap,
        title: marker.name,
        position: latLng
    });

    var infoWindow = new google.maps.InfoWindow({
        content: '<span>'+name+'</span><p>'+marker.address+'</p>'
    });

    google.maps.event.addListener(newMarker, 'click', function() {
        infoWindow.open(gmap, this);
    });

    // Zoom to bounds
    if(bounds[''+mapName] == undefined) {
        bounds[''+mapName] = new google.maps.LatLngBounds();
    }
    bounds[''+mapName].extend(latLng);
    gmap.fitBounds(bounds[''+mapName]);
}

/**
 * Adding markers on the google map by address
 * Format data addMarcers((string) 'mapName', (json) {marker1:{name:'Marker name', link:'http://www.site.com/page.html', address:'City, Address 23, 4577'}})
 */
function addMarkersOnMapByAddress(mapName, markers) {
    $(window).on('load', function() {
        var gmap = new google.maps.Map(document.getElementById('map_canvas_gmaps'+mapName), initOptionsMap());

        $.each(markers, function(i, val) {
            var geocoder = new google.maps.Geocoder();

            // Next line creates asynchronous request
            geocoder.geocode({address: val.address}, function(results, status) {
                // And this is function which processes response
                if (status == google.maps.GeocoderStatus.OK) {
                    val['lat'] = results[0].geometry.location.lat();
                    val['lng'] = results[0].geometry.location.lng();

                    addMarkerOnMap(gmap, mapName, val);
                }
                else {
                    console.log('Geocode was not successful for the following reason: '+status);
                }
            });
        });
    });
}

/**
 * Adding markers on the google map by coordinates
 * Format data addMarcers((string) 'mapName', (json) {marker1:{name:'Marker name', link:'http://www.site.com/page.html', address:'City, Address 23, 4577', 'lat':'0.454', 'lng':'4.574'}})
 */
function addMarkersOnMapByCoordinates(mapName, markers) {
    $(window).on('load', function() {
        var gmap = new google.maps.Map(document.getElementById('map_canvas_gmaps'+mapName), initOptionsMap());

        $.each(markers, function(i, val) {
            addMarkerOnMap(gmap, mapName, val);
        });
    });
}
