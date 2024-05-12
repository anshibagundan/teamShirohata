from django.urls import path

from .views import title_page

urlpatterns = [
    path('', title_page, name='title_page'),
]