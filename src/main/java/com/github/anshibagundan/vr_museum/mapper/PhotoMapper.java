package com.github.anshibagundan.vr_museum.mapper;


import com.github.anshibagundan.vr_museum.dto.PhotoDto;
import com.github.anshibagundan.vr_museum.entity.Photo;

public class PhotoMapper {
    public static PhotoDto mapToPhotoDto(Photo photo) {
        return new PhotoDto(
                photo.getId(),
                photo.getTitle(),
                photo.getDescription(),
                photo.getUser(),
                photo.getImg(),
                photo.getTime(),
                photo.getTag()
        );
    }

    public static Photo mapToPhoto(PhotoDto photoDto) {
        return new Photo(
                photoDto.getId(),
                photoDto.getTitle(),
                photoDto.getDescription(),
                photoDto.getUser(),
                photoDto.getImg(),
                photoDto.getTime(),
                photoDto.getTag()
        );
    }
}