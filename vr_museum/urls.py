from django.urls import include, path
from rest_framework.routers import DefaultRouter

from vr_museum_app.views import PhotoViewSet

router = DefaultRouter()
router.register(r'photos', PhotoViewSet)

urlpatterns = [
    path('', include(router.urls)),
]