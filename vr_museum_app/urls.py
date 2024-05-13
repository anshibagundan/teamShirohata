from django.conf import settings
from django.conf.urls.static import static
from django.contrib.auth import views as auth_views
from django.urls import path

from .views import get_photo, title_page, user_create, user_login

urlpatterns = [
    path('', user_login, name='login'),
    path('login_create/', user_create, name='login_create'),
    path('home/', title_page, name='title'),
    path('logout/', auth_views.LogoutView.as_view(), name='logout'),
    path('photo/', get_photo, name='photo')
] + static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)