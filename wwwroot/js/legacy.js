// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


/*
 * <div id="left-right-div-wrapper">
        <div id="left-col-div">
            <ul id="left-col-div-ul" lang="sv"
                style="padding-left: 0; margin-left: 0; list-style-position: inside;">

                <li><a class="allCategoryAnchor underlinetoggle noselect" style="font-size: 1.10em;">* [@categories.Sum(a => a.GetCount())]</a></li>

                @foreach (var entry in categories)
                {
                    <li id="@entry.Name.GetHashCode()" class="toggle">
                        <a class="mainCategoryAnchor underlinetoggle noselect" id="@entry.Name">@entry.DisplayName [@entry.GetCount()]</a>
                    </li>
                    <li>
                        <ul class="@entry.Name.GetHashCode()" style="display: none;">

                            @foreach (var sub in entry.SubCategories)
                            {
                                <li><a class="subCategoryAnchor underlinetoggle noselect" id="@sub.Name">@sub.DisplayName [@sub.Count]</a></li>
                            }
                        </ul>
                    </li>
                }
            </ul>
        </div> <!-- end of left-col-div -->

        <script type="text/javascript">
            $(document).ready(function () {
                $('.toggle').click(function () {
                    $('.' + $(this).attr('id')).toggle();
                });
                $('.underlinetoggle').click(function () {
                    $('.underlinetoggle').removeClass('underline');
                    $(this).toggleClass('underline');
                    if ($(this).hasClass('mainCategoryAnchor'))
                        $('#category').attr('value', $(this).attr('id'));
                    else if ($(this).hasClass('subCategoryAnchor'))
                        $('#category').attr('value', $('#' + $(this).parent('li').parent('ul').attr('class')).children('a').attr('id').concat('.').concat($(this).attr('id')));
                    else if ($(this).hasClass('allCategoryAnchor'))
                        $('#category').attr('value', null);
                }).mouseover(function () {
                    $(this).css('text-decoration', 'underline');
                }).mouseleave(function () {
                    $(this).css('text-decoration', 'none');
                });
                $('body').css('overflow', 'scroll');
            });
        </script>
 * 
 * 
 * 
 * <div class="tree">
                @for (int i = 0; i < categories.Count; i++)
                {
                    <ul>
                        <li>
                            <a href="#" class="a-group-toggle">
                                @Html.CheckBoxFor(category => true, new { @class = "tree-checkbox parent", @id = categories[i].Name })

                                <label for="@i">
                                    @Html.DisplayFor(category => categories[i].DisplayName)
                                </label>
                            </a>
                            <ul>
                                @for (int j = 0; j < categories[i].SubCategories.Count; j++)
                                {
                                    int k = 1 + j;
                                    @Html.HiddenFor(category => categories[i].SubCategories[j].Count)

                                    <li>
                                        <a href="#">
                                            @Html.CheckBoxFor(category => categories[i].SubCategories[j].Exists, new { @class = "tree-checkbox node-item", @id = i + "" + j })

                                            <label for="@i@j">
                                                @Html.DisplayFor(category => categories[i].SubCategories[j].DisplayName)
                                            </label>
                                        </a>
                                    </li>
                                }
                            </ul>
                        </li>
                    </ul>
                }
            </div> <!-- end of tree -->


  <div id="purchaseModal" class="modal fade" role="dialog">

        <div class="modal-dialog modal-dialog-centered">

            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Confirm Purchase</h4>
                    <button type="button" class="close float-right" data-dismiss="modal">&times;</button>
                </div>

                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="card">Credit Card Number</label>
                                <input name="card" id="card" type="text" />
                            </div>
                            <div class="form-group">
                                <label for="cvc">CVC</label>
                                <input id="cvc" type="text" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-success" data-dismiss="modal" onclick="postBid(getBid());">Purchase</button>
                </div>
            </div>

        </div>
    </div>

                       <!--

    <button id="modalBtn" class="btn btn-group-toggle" style="position: absolute; right: 20px;" onclick="$('#myModal').toggle();">User Profile</button>

    <div id="myModal" class="modal">

        <div class="modal-content">
            <span class="close" onclick="$('#myModal').hide();">&times;</span>
            @if (HttpContextAccessor.HttpContext.Request.Cookies["session:id"] != null &&
              SessionService.Get(HttpContextAccessor.HttpContext.Request.Cookies["session:id"].ToString()).Username != null)
            {
                <a asp-area="" asp-controller="" asp-action="">Settings</a>
                <a asp-area="" asp-controller="Account" asp-action="Logout">Logout</a>
            }
            else
            {
                <a asp-area="" asp-controller="Account" asp-action="Register">Register</a>
                <a asp-area="" asp-controller="Account" asp-action="Login" id="login">Login</a>
            }
        </div>
    </div>
        -->
 */