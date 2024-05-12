from django.shortcuts import render


def title_page(request):
    return render(request, 'index.html')