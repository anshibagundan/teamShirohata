from django.contrib import admin
from django.urls import include, path
from rest_framework.routers import DefaultRouter

from vr_museum_app.views import PhotoViewSet

router = DefaultRouter()
router.register(r'photos', PhotoViewSet)

urlpatterns = [
    path('a', include(router.urls)),
    path('', include('vr_museum_app.urls')),
    path('admin/', admin.site.urls),
]