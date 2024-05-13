from django.conf import settings
from django.conf.urls.static import static
from django.contrib import admin
from django.contrib.auth import views as auth_views
from django.urls import include, path
from rest_framework.routers import DefaultRouter

from vr_museum_app.views import (PhotoViewSet, get_photo, title_page,
                                 user_create, user_login)

router = DefaultRouter()
router.register(r'photos', PhotoViewSet)

urlpatterns = [
    path('', include(router.urls)),
    path('admin/', admin.site.urls),
    path('login/', user_login, name='login'),
    path('login_create/', user_create, name='login_create'),
    path('home/', title_page, name='title'),
    path('logout/', auth_views.LogoutView.as_view(), name='logout'),
    path('photo/', get_photo, name='photo')
] + static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)