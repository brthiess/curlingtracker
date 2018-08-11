/* Scoreboard */
var fullScreenScoreboardWidth = 719;


function showEvent(eventId){
	$('.competition-list-item').removeClass('active');
	$('.competition-list-item[data-id=' + eventId + ']').addClass('active');
	addLoadingToClass('scores-container-container');
	makeScoresContainerActive();
	getScoresView(eventId, null, function(viewHtml, error){
		if (typeof error == 'undefined'){
			$('.scores-container-container').html(viewHtml);
			currentCompetitionId = eventId;
			currentDrawId = $('.scores-container-container [data-draw-id]').attr('data-draw-id');
			updateBackButton(true);
		}
		removeLoadingFromClass('scores-container-container');
	});
}

function showGame(gameId, eventId){
	makeScoresContainerActive();
	addLoadingToClass('scores-container-container');
	getGameView(gameId, function(viewHtml, error){
		if (typeof error == 'undefined'){
			$('.scores-info-wrapper').html(viewHtml);
			updateBackButton(false, eventId);
		}
		removeLoadingFromClass('scores-container-container');
	});
}

function showDraw(drawId){
	console.log(drawId);
	addLoadingToClass('scores-container-container');
	getDrawView(drawId, function(viewHtml, error){
		if (typeof error == 'undefined'){
			$('.scores-games-wrapper').html(viewHtml);
		}
		removeLoadingFromClass('scores-container-container');
	})
}

function updateBackButton(isClosing, eventId){
	var onClickAttr = "";
	var backButton = $("[data-back-button]");
	if (isClosing){
		onClickAttr = "closeScores()";
		backButton.attr("data-back-action", "close");
	}
	else {
		onClickAttr = "showEvent('" + eventId + "');";
		backButton.attr("data-back-action", "back");
	}
	backButton.attr("onclick", onClickAttr);
}

function closeScores(){
	makeScoresContainerInActive();
}

function removeLoadingFromClass(className){
	$('.' + className + ' .loading').remove();
	$('.' + className).css('position', '');
}

function addLoadingToClass(className){
	$('.' + className).append('<div class="loading"><div class="spinner"></div></div>');
}
function makeScoresContainerActive(){
	$('.scores-container-container').addClass('active');
	if ($(window).width() < fullScreenScoreboardWidth){
		$('body').css('overflow', 'hidden');
	}
}

function makeScoresContainerInActive(){
	$('.scores-container-container').removeClass('active');
	$('body').css('overflow', '');
}

function showOnScoreboard(section) {
	$("[data-scoreboard-section]").removeClass('active');
	$("[data-scoreboard-section=" + section + "]").addClass('active');
}

function toggleMobileMenu(element) {
	$('.mobile-nav-container').toggleClass('hidden');
	$('body').toggleClass('overflow-hidden');
	toggleHtml(element, 'Close', 'Menu');
}

function toggleHtml(element, text1, text2) {
	if($(element).html() == text1) {
		$(element).html(text2);
	}
	else {
		$(element).html(text1);
	}
}

$(document).ready(function(){
	if($('competition-list').scrollLeft() <= 1){
		$('.left-arrow').addClass('hidden');
	}
	else if ($('.competition-list').length > 0 && ($('competition-list').scrollLeft() + $(window).width()) >= $('.competition-list')[0].scrollWidth) {
		$('.right-arrow').addClass('hidden');
	}
	$('.competition-list').scroll(function(){
		var scrollLeft = $(this).scrollLeft();
		if (scrollLeft <= 1){
			$('.left-arrow').addClass('hidden');
			$('.right-arrow').removeClass('hidden');
		}
		else if ((scrollLeft + $(window).width()) >= $('.competition-list')[0].scrollWidth) {
			$('.right-arrow').addClass('hidden');
			$('.left-arrow').removeClass('hidden');
		}
	});
});

/*modal*/
var modalOpen = false;
function showModalWithGameView(gameId, modalTitle) {
	showModal(modalTitle);
	modalLoad(true);
	getGameModalView(gameId, function(html, err){
		modalLoad(false);
		if(err == undefined) {
			$("#modal-content").html(html);
			if(!modalHistoryShowsItsOpen()) {
				history.pushState({modal: "open", title: modalTitle, gameId: gameId}, "", "#score");
			}
		}
		else {
			//error
		}
		
	});	
}
function showModal(modalTitle){
	$("#modal-overlay").addClass("visible");
	$("#modal-title").text(modalTitle);
	$("body").css("overflow", "hidden");
	modalOpen = true;
}
function closeModal(){
	$("#modal-overlay").removeClass("visible");
	$("body").css("overflow", "");
	if(modalHistoryShowsItsOpen()){
		history.back();
	}
	$("#modal-content").html("");
	modalOpen = false;
}
function modalLoad(showLoading){
	if(showLoading){
		$("#modal-content-container").addClass("loading");
	}
	else {
		$("#modal-content-container").removeClass("loading");
	}
}
//return true if the current history state suggests that the modal is open
function modalHistoryShowsItsOpen(){
	if(history.state !== null && history.state.modal == 'open') {
		return true;
	}
	else {
		return false;
	}
}
window.onpopstate = function(event) {
	if(modalOpen == true){
		closeModal();
	}
	console.log(history.state);
	if(modalOpen == false && modalHistoryShowsItsOpen()){
		showModalWithGameView(history.state.gameId, history.state.modalTitle);
	}

}


function updateDrawId(competitionId, drawId){
	$('.scores-info-container').addClass('loading');
	getScoresGamesView(competitionId, drawId, function(viewHtml){
		$('.scores-games-wrapper').replaceWith(viewHtml);
		currentCompetitionId = competitionId;
		currentDrawId = $('.scores-container [data-draw-id]').attr('data-draw-id');
		$('.scores-info-container').removeClass('loading');
	});
}

