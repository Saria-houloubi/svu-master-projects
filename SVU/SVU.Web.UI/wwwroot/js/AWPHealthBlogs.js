//
//Will be called once the document is loaded
//
$(function () {
    //Load the blogs
    loadMoreBlogs(undefined);
});

//Pagination variables
var loadAmount = 10;
var loadStart = 0;
//
//loads more blogs based on the sent variables
//
function loadMoreBlogs(element) {
    if (element)
        changeButtonStatus(element, 'loading');

    loadBlogs(loadStart, loadAmount, onLoadCallBack);
}

//
//Will be called once the load is done
//  showLoadMore : a flag to check if we need to show the load more button
//
function onLoadCallBack(showLoadMore) {
    loadStart = document.getElementsByClassName('card').length;

    if (showLoadMore) {
        $('#load_More_btn')[0].classList.remove('collapse');
    } else {
        $('#load_More_btn')[0].classList.add('collapse');
    }

    changeButtonStatus($('#load_More_btn')[0], 'done');
}

//
//Creates a blog card in hmtl
//  blog : the blog to fill up the information from
//
//
function createBlogCard(blog) {
    //Create the card div
    var card = document.createElement('div');
    card.classList.add('card', 'd-inline-block', 'm-lg-1', 'my-2', 'text-left');
    card.style.setProperty('width', '18rem');
    //Create the image for the card
    var image = document.createElement('img');
    image.classList.add('card-img-top');
    image.setAttribute('src', blog.hasThumbnail ? blog.thumbnailBase64 : '/img/_NoImage.svg');
    //Create the card body
    var cardBody = document.createElement('div');
    cardBody.classList.add('card-body');
    //Create card title
    var cardTitle = document.createElement('div')
    cardTitle.classList.add('card-title');
    cardTitle.textContent = blog.title;
    //Craete the card text
    var cardText = document.createElement('div')
    cardText.classList.add('card-title');
    cardText.textContent = blog.previewContent;
    //Create blog link
    var blogLink = document.createElement('a');
    blogLink.classList.add('btn', 'btn-primary');
    blogLink.textContent = 'Read More';
    blogLink.href = `/awphealth/blog/${blog.id}`
    //Craete the card footer
    var cardFooter = document.createElement('div');
    cardFooter.classList.add('card-footer', 'text-muted');
    //Create footer content
    var footerContent = document.createElement('p');
    footerContent.innerHTML = `<i class="fa fa-eye"></i> ${blog.visitCout}<br/> <i class="fa fa-calendar"></i> ${blog.creationDate}`;

    //Form up the elements
    card.appendChild(image);

    cardBody.appendChild(cardTitle);
    cardBody.appendChild(cardText);
    cardBody.appendChild(blogLink);

    card.appendChild(cardBody);

    cardFooter.appendChild(footerContent);

    card.appendChild(cardFooter);

    return card;
}


//
//starts loading the blog data
//  start : the starting point to load from
//  count : the count of blogs  to load
//
function loadBlogs(start, count, onLoadCallback) {
    $.ajax('/AWPHealth/GetBlogs',
        {
            method: 'GET',
            data: {
                start,
                count,
                loadImages: true
            },
            success: function (data) {
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        $('#blog_list')[0].appendChild(createBlogCard(data[i]));
                    }
                } else {
                    if (loadStart === 0) {
                        var image = document.createElement('img');
                        image.setAttribute('src', '/img/empty.svg');
                        $('#blog_list')[0].appendChild(image);
                    }
                }
                onLoadCallback(data.length === count);
            },
            error: function (err) {
                showAlert('danger', err.responseJSON.message);

                onLoadCallback(false);
            }
        }).done(function () {
            $('#loading_part')[0].classList.add('collapse');

        });
}
