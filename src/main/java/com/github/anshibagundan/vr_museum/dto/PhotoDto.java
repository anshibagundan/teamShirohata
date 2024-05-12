package com.github.anshibagundan.vr_museum.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class PhotoDto {
    private Long id;
    private String title;
    private String description;
    private String user;
    private String img;
    private String time;
    private String tag;
}
