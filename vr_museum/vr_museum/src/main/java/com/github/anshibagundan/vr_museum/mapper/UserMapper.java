package com.github.anshibagundan.vr_museum.mapper;

import com.github.anshibagundan.vr_museum.dto.UserDto;
import com.github.anshibagundan.vr_museum.entity.User;

public class UserMapper {
    public static UserDto mapToUserDto(User user) {
        return new UserDto(
                user.getId(),
                user.getUsername(),
                user.getPassword()
        );
    }

    public static User mapToUser(UserDto userDto) {
        return new User(
                userDto.getId(),
                userDto.getUsername(),
                userDto.getPassword()
        );
    }
}
