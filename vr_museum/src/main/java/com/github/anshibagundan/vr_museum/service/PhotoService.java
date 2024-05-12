package com.github.anshibagundan.vr_museum.service;

import com.github.anshibagundan.vr_museum.dto.PhotoDto;

import java.util.List;

public interface PhotoService {
    PhotoDto createPhoto(PhotoDto photoDto);

    PhotoDto getPhotoById(Long photoId);

    List<PhotoDto> getAllPhotos();

    PhotoDto updatePhoto(Long photoId, PhotoDto updatedPhoto);

    void deletePhoto(Long photoId);
}
