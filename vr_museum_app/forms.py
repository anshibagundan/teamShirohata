from django import forms

from .models import Photo


class PhotoForm(forms.ModelForm):
   class Meta:
       model = Photo
       fields = ('title', 'detailed_title', 'time', 'content', 'tag')
       widgets = {
           'title': forms.TextInput(attrs={'class': 'form-control'}),
           'detailed_title': forms.TextInput(attrs={'class': 'form-control'}),
           'time': forms.DateInput(attrs={'class': 'form-control', 'type': 'date'}),
           'content': forms.FileInput(attrs={'class': 'form-control'}),
           'tag': forms.TextInput(attrs={'class': 'form-control'}),
       }