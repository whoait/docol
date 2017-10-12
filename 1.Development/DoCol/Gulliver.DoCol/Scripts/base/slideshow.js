/* Width and height of the tiles in pixels: */
var tabwidth = 60, tabheight = 60;

var current = {};

$(document).ready(function () {
	/* This code is executed after the DOM has been completely loaded */
	$("body").keyup(function (e) {
		// left arrow
		if ((e.keyCode || e.which) == 37) {
			// do something
			prev();
			/* Clearing the autoadvance if we click one of the arrows */
			clearInterval(auto);
		}
		// right arrow
		if ((e.keyCode || e.which) == 39) {
			// do something
			next();
			clearInterval(auto);
		}
		// esc
		if ((e.keyCode || e.which) == 27) {
			$('.modal').modal('hide');
		}
		this.clearQueue();
	});

	$('.arrow.left').click(function () {
		prev();

		/* Clearing the autoadvance if we click one of the arrows */
		clearInterval(auto);
	});

	$('.arrow.right').click(function () {
		next();
		clearInterval(auto);
	});

	/* Preloading all the slide images: */

	for (var i = 0; i < slides.length; i++) {
		(new Image()).src = slides[i]['ImgPath'];
	}

	/* Shoing the first one on page load: */
	transition(curentIndex);

	/* Setting auto-advance every 10 seconds */

	var auto;

	//auto = setInterval(function () {
	//	next();
	//}, 10 * 1000);
});

function transition(id) {
	/* This function shows the individual slide. */

	if (!slides[id - 1]) return false;

	if (current.id) {
		/* If the slide we want to show is currently shown: */
		if (current.id == id) return false;

		/* Moving the current slide layer to the top: */
		current.layer.css('z-index', 10);

		/* Removing all other slide layers that are positioned below */
		$('.mosaic-slide').not(current.layer).remove();
	}

	/* Creating a new slide and filling it with generateGrid: */
	var newLayer = $('<div class="mosaic-slide">').html(generateGrid({ rows: 7, cols: 8, image: slides[id - 1]['ImgPath'] }));

	/* Moving it behind the current slide: */
	newLayer.css('z-index', 1);

	$('#mosaic-slideshow').append(newLayer);

	if (current.layer) {
		/* Hiding each tile of the current slide, exposing the new slide: */
		$('.tile', current.layer).each(function (i) {
			var tile = $(this);
			setTimeout(function () {
				tile.css('visibility', 'hidden');
			}, i * 10);
		})
	}

	/* Adding the current id and newLayer element to the current object: */
	current.id = id;
	current.layer = newLayer;

	// Disable next
	if (current.id && current.id == slides.length) {
		$('.arrow.right').attr('disabled', 'disabled');
		$('.arrow.right').css("display", "none");
	}

	// Disable prev
	if (current.id && current.id == 1) {
		$('.arrow.left').attr('disabled', 'disabled');
		$('.arrow.left').css("display", "none");
	}

	// Set message to view
	$('#message').text(slides[id - 1]['ImgTitle']);
}

function next() {
	if (current.id && current.id < slides.length) {

		transition(current.id % slides.length + 1);

		// Enable prev
		$('.arrow.left').prop("disabled", false);
		$('.arrow.left').show();
	}

	// Disable next
	if (current.id == slides.length) {
		$('.arrow.right').attr('disabled', 'disabled');
		$('.arrow.right').css("display", "none");
	}
}

function prev() {
	if (current.id && current.id > 1) {
		transition((current.id + (slides.length - 2)) % slides.length + 1);

		// Enable next
		$('.arrow.right').prop("disabled", false);
		$('.arrow.right').show();
	}

	// Disable prev
	if (current.id == 1) {
		$('.arrow.left').attr('disabled', 'disabled');
		$('.arrow.left').css("display", "none");
	}

}

function generateGrid(param) {
	/* This function generates the tile grid, with each tile containing a part of the slide image */

	/* Creating an empty jQuery object: */
	var elem = $([]), tmp;

	tmp = $('<div>', {
		"class": "tile",
		"css": {
			"background": '#FFF url(' + param.image + ') no-repeat',
			"background-position": 'center',
			"height": '420px',
			"width": '480px'
		}
	});
	/* Adding a clearing element at the end of each line. This will clearly divide the divs into rows: */
	elem = elem.add(tmp).fadeIn(300);

	/* Adding a clearing element at the end of each line. This will clearly divide the divs into rows: */
	elem = elem.add('<div class="clear"></div>');
	return elem;
}